using Spectre.Console.Cli;

namespace ZenMoneyPlus.Cli.Settings;

public abstract class CliSettingsBase : CommandSettings
{
    [CommandOption("-t|--token")]
    public string? Token { get; set; }
}