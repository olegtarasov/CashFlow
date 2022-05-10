using System.Reflection;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Sinks.SystemConsole.Themes;

namespace ZenMoneyPlus.Helpers;

/// <summary>
/// Console logging sink mode.
/// </summary>
public enum ConsoleMode
{
    /// <summary>
    /// Don't log to console.
    /// </summary>
    None,

    /// <summary>
    /// Log to system console with no colors.
    /// </summary>
    System,

    /// <summary>
    /// Log to system console with colors.
    /// </summary>
    Colored
}

/// <summary>
/// A class that configures logging with Serilog.
/// </summary>
public static class LoggerConfig
{
    private const string FileTemplate =
        "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}][{SourceContext}] {Message:lj}{NewLine}{Exception}";

    private const string ConsoleTemplate = "{Timestamp:HH:mm:ss} [{Level:u4}] {Message:lj}{NewLine}{Exception}";

    private static readonly object Locker = new();

    /// <summary>
    /// Configures Serilog.
    /// </summary>
    /// <param name="file">Indicates whether to write logs to a file.</param>
    /// <param name="console">Console logging mode.</param>
    /// <param name="fileName">File name to write logs to. Uses [Assembly.Name].txt when <code>null</code>.</param>
    /// <param name="defaultLevel">Default logging level.</param>
    /// <param name="levelSwitch">An optional switch that can control logging level at runtime.</param>
    public static void ConfigureSerilog(
        bool file = true,
        ConsoleMode console = ConsoleMode.System,
        string? fileName = null,
        LogEventLevel defaultLevel = LogEventLevel.Debug,
        LoggingLevelSwitch? levelSwitch = null)
    {
        if (Log.Logger != null && Log.Logger.GetType().Name != "SilentLogger")
            return;

        lock (Locker)
        {
            if (Log.Logger != null && Log.Logger.GetType().Name != "SilentLogger")
                return;

            var level = defaultLevel;
            string? logLevel = Environment.GetEnvironmentVariable("LOGLEVEL");
            if (!string.IsNullOrEmpty(logLevel))
                if (!Enum.TryParse(logLevel, true, out level))
                    level = defaultLevel;

            var config = new LoggerConfiguration();
            if (levelSwitch != null)
                config = config.MinimumLevel.ControlledBy(levelSwitch);
            else
                config = config.MinimumLevel.Is(level)
                               .MinimumLevel.Override("Microsoft", LogEventLevel.Warning);

            config = config
                     .Enrich.FromLogContext()
                     .Enrich.WithExceptionDetails();

            if (file)
            {
                if (string.IsNullOrEmpty(fileName))
                {
                    string? assemblyName = Assembly.GetCallingAssembly().GetName().Name;
                    fileName = Path.Combine("logs", $"{assemblyName ?? "log"}.txt");
                }

                config = config.WriteTo.File(
                    fileName,
                    outputTemplate: FileTemplate,
                    fileSizeLimitBytes: 1024 * 1024 * 5,
                    retainedFileCountLimit: 10,
                    rollOnFileSizeLimit: true);
            }

            if (console == ConsoleMode.System || console == ConsoleMode.Colored)
            {
                config = config.WriteTo.Console(
                    theme: console == ConsoleMode.System ? ConsoleTheme.None : SystemConsoleTheme.Colored,
                    outputTemplate: ConsoleTemplate);
            }
            // else if (console == ConsoleMode.Spectre)
            //     config = config.WriteTo.SpectreConsole(ConsoleTemplate, level);

            Log.Logger = config.CreateLogger();
        }
    }
}