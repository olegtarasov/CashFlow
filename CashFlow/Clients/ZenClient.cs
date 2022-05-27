using CashFlow.Helpers;
using CashFlow.Messages;
using CashFlow.Models;
using CashFlow.Services;
using RestSharp;
using RestSharp.Serializers.Json;

namespace CashFlow.Clients;

internal class ZenClient
{
    private readonly ILogger<ZenClient> _logger;

    private readonly IAuthTokenProvider _tokenProvider;

    protected readonly RestClient Client;

    /// <summary>
    /// Ctor.
    /// </summary>
    public ZenClient(IAuthTokenProvider tokenProvider, ILogger<ZenClient> logger)
    {
        _tokenProvider = tokenProvider;
        _logger = logger;

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

        _logger.LogInformation("Sending sync request");

        var resopnse = await Client.ExecuteAsync<SyncResponse>(request);
        if (!resopnse.IsSuccessful)
            throw new InvalidOperationException("Request was not successfull");

        return resopnse.Data ?? throw new InvalidOperationException("Response object is null");
    }

    public async Task<ReceiptModel?> GetReceipt(string qrCode)
    {
        var request = await CreateRequest("parse_qr_code");
        request.AddJsonBody(new { code = qrCode });

        var response = await Client.ExecuteAsync<ReceiptModel>(request);
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