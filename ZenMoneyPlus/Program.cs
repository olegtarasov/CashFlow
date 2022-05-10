using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Spectre.Console.Cli;
using ZenMoneyPlus.Cli;
using ZenMoneyPlus.Cli.Commands;
using ZenMoneyPlus.Clients;
using ZenMoneyPlus.Data;
using ZenMoneyPlus.Data.Entities;
using ZenMoneyPlus.Helpers;
using ZenMoneyPlus.Services;

LoggerConfig.ConfigureSerilog(false, Debugger.IsAttached ? ConsoleMode.System : ConsoleMode.Colored);

var services = new ServiceCollection();
services.AddLogging(builder => builder.AddSerilog());
services.AddTransient<IAuthTokenProvider, DbAuthTokenProvider>();
services.AddTransient<ZenService>();
services.AddDbContext<ZenContext>();
services.AddTransient<ZenClient>();
services.AddAutoMapper(cfg =>
                       {
                           cfg.CreateMap<Tag, Tag>()
                              .ForMember(x => x.ChildrenTags, m => m.Ignore())
                              .ForMember(x => x.ParentTag, m => m.Ignore())
                              .ForMember(x => x.TransactionTags, m => m.Ignore());

                           cfg.CreateMap<Transaction, Transaction>()
                              .ForMember(x => x.Tag, m => m.Ignore())
                              .ForMember(x => x.TransactionTags, m => m.Ignore());
                       });

var registrar = new TypeRegistrar(services);

var logger = Log.ForContext<Program>();
var app = new CommandApp(registrar);

app.Configure(config =>
              {
                  config.AddCommand<SyncCommand>("sync");
                  config.AddCommand<ReceiptsCommand>("receipts");
                  config.SetExceptionHandler(exception => { logger.Fatal(exception, "Fatal exception"); });
              });

return app.Run(args);