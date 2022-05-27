using CashFlow.Services;
using Spectre.Console.Cli;

namespace CashFlow.Cli.Commands;

internal class DepersonalizeCommand : AsyncCommand
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