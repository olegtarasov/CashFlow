using Spectre.Console.Cli;
using ZenMoneyPlus.Cli.Settings;
using ZenMoneyPlus.Services;

namespace ZenMoneyPlus.Cli.Commands;

public class SyncCommand : AsyncCommand<SyncSettings>
{
    private readonly IAuthTokenProvider _tokenProvider;
    private readonly ZenService _zenService;

    public SyncCommand(IAuthTokenProvider tokenProvider, ZenService zenService)
    {
        _tokenProvider = tokenProvider;
        _zenService = zenService;
    }

    public override async Task<int> ExecuteAsync(CommandContext context, SyncSettings settings)
    {
        await _zenService.MigrateDatabase();
        
        if (!string.IsNullOrEmpty(settings.Token))
        {
            await _tokenProvider.UpdateToken(settings.Token);
        }

        await _zenService.Sync();

        return 0;
    }
}