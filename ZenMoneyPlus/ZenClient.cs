using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ZenMoneyPlus.Messages;
using ZenMoneyPlus.Models;

namespace ZenMoneyPlus
{
    public class ZenClient : IDisposable, IAsyncDisposable
    {
        private readonly string _authToken;
        private readonly HttpClient _client;

        public ZenClient(string authToken)
        {
            _authToken = authToken;
            _client = CreateClient();
        }

        public async Task PullData(long timestamp)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "v8/diff");
            request.Content = new StringContent("{" +
                $"\"currentClientTimestamp\": {DateTimeOffset.UtcNow.ToUnixTimeSeconds()}," +
                "\"currentClientTimezoneOffset\": 180," +
                $"\"serverTimestamp\": {timestamp}" +
                "}", Encoding.UTF8, MediaTypeNames.Application.Json);

            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<SyncResponse>(content, new JsonSerializerOptions
            {
                IgnoreNullValues = true,
                PropertyNameCaseInsensitive = true,
            });
        }

        private HttpClient CreateClient()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri("https://api.zenmoney.ru")
            };
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _authToken);
            client.DefaultRequestHeaders.UserAgent.TryParseAdd("okhttp/3.6.0");
            client.DefaultRequestHeaders.TryAddWithoutValidation("zen-app", "Android");
            client.DefaultRequestHeaders.TryAddWithoutValidation("zen-app-version", "5.12.0");
            client.DefaultRequestHeaders.TryAddWithoutValidation("zen-app-build", "609");

            return client;
        }

        public void Dispose()
        {
            _client.Dispose();
        }

        public ValueTask DisposeAsync()
        {
            Dispose();
            return new ValueTask(Task.CompletedTask);
        }
    }
}