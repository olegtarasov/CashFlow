using Spectre.Console.Cli;

namespace ZenMoneyPlus.Cli.Settings;

internal abstract class CliSettingsBase : CommandSettings
{
    [CommandOption("-t|--token")]
    public string? Token { get; set; }
}