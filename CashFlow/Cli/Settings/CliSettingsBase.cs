using Spectre.Console.Cli;

namespace CashFlow.Cli.Settings;

internal abstract class CliSettingsBase : CommandSettings
{
    [CommandOption("-t|--token")]
    public string? Token { get; set; }
}