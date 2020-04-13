using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using ZenMoneyPlus.Models;

namespace ZenMoneyPlus
{
    internal class Program
    {
        private static readonly ILogger _log = Log.ForContext<Program>();
        
        internal static async Task Main(string[] args)
        {
            LoggerConfig.ConfigureSerilog();
            
            string token = await Init(args);
            if (string.IsNullOrEmpty(token))
            {
                _log.Error("Couldn't get auth token. Exiting.");
                return;
            }
            
            await ParseRoot(args, token);
        }

        private static async Task<string> Init(string[] args)
        {
            await using var ctx = new ZenContext();
            await ctx.Database.MigrateAsync();
            var tokenSetting = await ctx.Settings.FindAsync(SettingCodes.AuthToken);

            if (tokenSetting == null || string.IsNullOrEmpty(tokenSetting.Value))
            {
                string token = FindOption("--token", args);
                if (string.IsNullOrEmpty(token))
                {
                    Console.WriteLine("Auth token not found in DB and not specified at command line.");
                    Console.Write("Enter an auth token: ");
                    token = Console.ReadLine();
                }
                
                if (string.IsNullOrEmpty(token))
                {
                    return null;
                }

                if (tokenSetting == null)
                {
                    tokenSetting = new Setting
                    {
                        Code = SettingCodes.AuthToken,
                        Value = token
                    };
                    ctx.Settings.Add(tokenSetting);
                }
                else
                {
                    tokenSetting.Value = token;
                }

                await ctx.SaveChangesAsync();
            }

            return tokenSetting.Value;
        }
        
        private static Task ParseRoot(string[] args, string token)
        {
            if (IsHelp(args))
            {
                return Help();
            }

            return args[0].ToLower() switch
            {
                "cat" => ParseCat(Pop(args)),
                "sync" => App.Sync.SyncData(token),
                _ => Help()
            };

            Task Help()
            {
                Console.WriteLine("Available commands:\n" +
                                  "\tcat\n" +
                                  "\tsync");

                return Task.CompletedTask;
            }
        }

        private static Task ParseCat(string[] args)
        {
            if (args.Length == 0)
            {
                return App.Categories.List();
            }
            
            if (IsHelp(args))
            {
                return Help();
            }

            return args[0].ToLower() switch
            {
                "ls" => App.Categories.List(),
                _ => App.Categories.List()
            };

            Task Help()
            {
                Console.WriteLine("Usage: cat [subcommand]\n" +
                                  "Available subcommands:\n" +
                                  "\tls");
                return Task.CompletedTask;
            }
        }

        private static string[] Pop(string[] args) => args.Length == 0 ? Array.Empty<string>() : args[1..];
        
        private static bool IsHelp(string[] args) =>
            args.Length == 0 || args[0].ToLower() == "--help" || args[0].ToLower() == "-h";

        private static string FindOption(string option, string[] args)
        {
            int idx = -1;
            string lOption = option.ToLower();
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].ToLower() == lOption)
                    idx = i;
            }

            if (idx == -1 || args.Length == idx + 1)
                return null;

            return args[idx + 1];
        }
    }
}