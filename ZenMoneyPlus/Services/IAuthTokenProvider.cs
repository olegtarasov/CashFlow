namespace ZenMoneyPlus.Services;

public interface IAuthTokenProvider
{
    Task<string> GetAuthToken();
    Task UpdateToken(string token);
}