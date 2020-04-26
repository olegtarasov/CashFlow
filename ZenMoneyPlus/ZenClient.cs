using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Serilog;
using ZenMoneyPlus.Messages;
using ZenMoneyPlus.Models;

namespace ZenMoneyPlus
{
    public class ZenClient : IDisposable, IAsyncDisposable
    {
        private static readonly ILogger _log = Log.ForContext<ZenClient>();
        
        private readonly string _authToken;
        private readonly HttpClient _client;

        public ZenClient(string authToken)
        {
            _authToken = authToken;
            _client = CreateClient();
        }

        public async Task<SyncResponse> Sync(long timestamp)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "v8/diff");
            request.Content = new StringContent("{" +
                $"\"currentClientTimestamp\": {DateTimeOffset.UtcNow.ToUnixTimeSeconds()}," +
                "\"currentClientTimezoneOffset\": 180," +
                $"\"serverTimestamp\": {timestamp}" +
                "}", Encoding.UTF8, MediaTypeNames.Application.Json);

            _log.Information("Sending sync request");
            var response = await _client.SendAsync(request);
            _log.Information("Received response: {0}, {1}", response.StatusCode, response.ReasonPhrase);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            _log.Information("Deserializing response");
            var data = JsonSerializer.Deserialize<SyncResponse>(content, new JsonSerializerOptions
            {
                IgnoreNullValues = true,
                PropertyNameCaseInsensitive = true,
            });

            return data;
        }

        public async Task<Receipt> GetReceipt(string qrCode)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "parse_qr_code");
            request.Content = new StringContent($"{{\"code\": \"{qrCode}\"}}", Encoding.UTF8, MediaTypeNames.Application.Json);

            var response = await _client.SendAsync(request);
            if (!response.IsSuccessStatusCode)
                return null;
            var content = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<Receipt>(content, new JsonSerializerOptions
            {
                IgnoreNullValues = true,
                PropertyNameCaseInsensitive = true,
            });

            return data;
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