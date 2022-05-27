using CashFlow.Cli.Settings;
using CashFlow.Services;
using Spectre.Console.Cli;

namespace CashFlow.Cli.Commands;

internal class SyncCommand : AsyncCommand<SyncSettings>
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
        if (!string.IsNullOrEmpty(settings.Token))
        {
            await _tokenProvider.UpdateToken(settings.Token);
        }

        await _zenService.Sync();

        return 0;
    }
}