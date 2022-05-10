using System.Net.Mime;
using System.Text;
using System.Text.Json;
using RestSharp;
using RestSharp.Serializers.Json;
using Serilog;
using ZenMoneyPlus.Data.Entities;
using ZenMoneyPlus.Helpers;
using ZenMoneyPlus.Messages;
using ZenMoneyPlus.Services;

namespace ZenMoneyPlus.Clients;

public class ZenClient
{
    private static readonly ILogger Log = Serilog.Log.ForContext<ZenClient>();

    private readonly IAuthTokenProvider _tokenProvider;
        
    protected readonly RestClient Client;
        

    public ZenClient(IAuthTokenProvider tokenProvider)
    {
        _tokenProvider = tokenProvider;

        Client = new RestClient(new RestClientOptions("https://api.zenmoney.ru")
                                {
                                    ThrowOnDeserializationError = true
                                })
            .UseSystemTextJson(JsonHelper.DefaultOptions);
    }

    public async Task<SyncResponse> Sync(long timestamp)
    {
        var request = await CreateRequest("v8/diff");
        request.AddJsonBody(new SyncRequest(DateTimeOffset.UtcNow.ToUnixTimeSeconds(), timestamp));

        Log.Information("Sending sync request");

        var resopnse = await Client.ExecuteAsync<SyncResponse>(request);
        if (!resopnse.IsSuccessful)
            throw new InvalidOperationException("Request was not successfull");

        return resopnse.Data ?? throw new InvalidOperationException("Response object is null");
    }

    public async Task<Receipt?> GetReceipt(string qrCode)
    {
        var request = await CreateRequest("parse_qr_code");
        request.AddJsonBody(new { code = qrCode });
        
        var response = await Client.ExecuteAsync<Receipt>(request);
        if (!response.IsSuccessful)
            return null;
        
        return response.Data;
    }

    /// <summary>
    /// Creates a new request. May be extended with additional headers. 
    /// </summary>
    protected virtual async Task<RestRequest> CreateRequest(string resource)
    {
        var request = new RestRequest(resource, Method.Post);
        request.AddOrUpdateHeader("Authorization", $"Bearer {await _tokenProvider.GetAuthToken()}");
        request.AddOrUpdateHeader("User-Agent", "Zenmoney/i5.0.1-431");

        return request;
    }
}