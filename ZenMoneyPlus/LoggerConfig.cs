using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

namespace ZenMoneyPlus
{
    public class LoggerConfig
    {
        private const string Template =
            "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}][{SourceContext}] {Message:lj}{NewLine}{Exception}";

        private static readonly object Locker = new object();
        
        public static void ConfigureSerilog(LogEventLevel level = LogEventLevel.Debug)
        {
            if (Log.Logger != null && Log.Logger.GetType().Name != "SilentLogger")
            {
                return;
            }

            lock (Locker)
            {
                if (Log.Logger != null && Log.Logger.GetType().Name != "SilentLogger")
                {
                    return;
                }

                var config = new LoggerConfiguration()
                    .MinimumLevel.Is(level)
                    .Enrich.FromLogContext();

                
                config = config.WriteTo.Console(theme: ConsoleTheme.None);
                

                Log.Logger = config.CreateLogger();
            }
        }
    }
}