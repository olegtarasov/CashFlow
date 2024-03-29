﻿using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Spectre.Console.Cli;
using CashFlow.Cli;
using CashFlow.Cli.Commands;
using CashFlow.Clients;
using CashFlow.Data;
using CashFlow.Data.Entities;
using CashFlow.Helpers;
using CashFlow.Models;
using CashFlow.Services;

LoggerConfig.ConfigureSerilog(false, Debugger.IsAttached ? ConsoleMode.System : ConsoleMode.Colored);

var services = new ServiceCollection();
services.AddLogging(builder => builder.AddSerilog());
services.AddTransient<IAuthTokenProvider, DbAuthTokenProvider>();
services.AddTransient<ZenService>();
services.AddTransient<DepersonalizationService>();
services.AddDbContext<ZenContext>();
services.AddTransient<ZenClient>();
services.AddAutoMapper(cfg =>
                       {
                           cfg.CreateMap<AccountModel, Account>();
                           cfg.CreateMap<TransactionModel, Transaction>()
                              .ForMember(x => x.IncomeAccount, x => x.Ignore())
                              .ForMember(x => x.OutcomeAccount, x => x.Ignore());
                           cfg.CreateMap<TagModel, Tag>();
                           cfg.CreateMap<ReceiptModel, Receipt>();
                           cfg.CreateMap<ReceiptItemModel, ReceiptItem>();
                       });

var registrar = new TypeRegistrar(services);

var logger = Log.ForContext<Program>();

logger.Information("Migrating database");
var ctx = new ZenContext();
await ctx.Database.MigrateAsync();

var app = new CommandApp(registrar);

app.Configure(config =>
              {
                  config.AddCommand<SyncCommand>("sync");
                  config.AddCommand<ReceiptsCommand>("receipts");
                  config.AddCommand<DepersonalizeCommand>("depersonalize");
                  config.AddCommand<ServeCommand>("serve");
                  config.SetExceptionHandler(exception => { logger.Fatal(exception, "Fatal exception"); });
              });

return app.Run(args);