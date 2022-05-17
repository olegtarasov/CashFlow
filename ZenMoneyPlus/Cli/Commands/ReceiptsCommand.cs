using Spectre.Console.Cli;
using ZenMoneyPlus.Cli.Settings;
using ZenMoneyPlus.Services;

namespace ZenMoneyPlus.Cli.Commands;

public class ReceiptsCommand : AsyncCommand<ReceptsSettings>
{
    private readonly IAuthTokenProvider _tokenProvider;
    private readonly ZenService _zenService;

    public ReceiptsCommand(IAuthTokenProvider tokenProvider, ZenService zenService)
    {
        _tokenProvider = tokenProvider;
        _zenService = zenService;
    }

    public override async Task<int> ExecuteAsync(CommandContext context, ReceptsSettings settings)
    {
        if (!string.IsNullOrEmpty(settings.Token))
        {
            await _tokenProvider.UpdateToken(settings.Token);
        }

        await _zenService.GetMissingReceipts();

        return 0;
    }
}