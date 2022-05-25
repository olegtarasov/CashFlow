using Spectre.Console.Cli;
using ZenMoneyPlus.Services;

namespace ZenMoneyPlus.Cli.Commands;

public class DepersonalizeCommand : AsyncCommand
{
    private readonly DepersonalizationService _depersonalizationService;

    public DepersonalizeCommand(DepersonalizationService depersonalizationService)
    {
        _depersonalizationService = depersonalizationService;
    }

    public override async Task<int> ExecuteAsync(CommandContext context)
    {
        await _depersonalizationService.Depersolaize();

        return 0;
    }
}