using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Spectre.Console.Cli;
using CashFlow.Cli;
using CashFlow.Cli.Commands;
using CashFlow.Data;
using CashFlow.Helpers;

LoggerConfig.ConfigureSerilog(false, Debugger.IsAttached ? ConsoleMode.System : ConsoleMode.Colored);

var services = new ServiceCollection();
services.AddLogging(builder => builder.AddSerilog());
services.AddDbContext<ZenContext>();

var registrar = new TypeRegistrar(services);

var logger = Log.ForContext<Program>();

logger.Information("Migrating database");
var ctx = new ZenContext();
await ctx.Database.MigrateAsync();

var app = new CommandApp(registrar);

app.Configure(config =>
              {
                  config.AddCommand<ServeCommand>("serve");
                  config.SetExceptionHandler(exception => { logger.Fatal(exception, "Fatal exception"); });
              });

return app.Run(args);