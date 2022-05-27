using CashFlow.Data;
using CashFlow.Data.Entities;
using CashFlow.Clients;

namespace CashFlow.Services;

internal class DbAuthTokenProvider : IAuthTokenProvider
{
    private readonly ZenContext _context;

    public DbAuthTokenProvider(ZenContext context)
    {
        _context = context;
    }

    public async Task<string> GetAuthToken()
    {
        var tokenSetting = await _context.Settings.FindAsync(SettingCodes.AuthToken);
        if (tokenSetting == null)
            throw new InvalidOperationException("Token was not found");

        return tokenSetting.Value;
    }

    public async Task UpdateToken(string token)
    {
        var tokenSetting = await _context.Settings.FindAsync(SettingCodes.AuthToken);
        if (tokenSetting == null)
        {
            tokenSetting = new Setting { Code = SettingCodes.AuthToken, Value = token };
            await _context.Settings.AddAsync(tokenSetting);
        }

        tokenSetting.Value = token;

        await _context.SaveChangesAsync();
    }
}