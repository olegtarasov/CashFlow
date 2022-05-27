namespace CashFlow.Services;

internal interface IAuthTokenProvider
{
    Task<string> GetAuthToken();
    Task UpdateToken(string token);
}