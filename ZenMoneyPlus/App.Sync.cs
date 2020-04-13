using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Serilog;
using ZenMoneyPlus.Models;

namespace ZenMoneyPlus
{
    public static partial class App
    {
        public static class Sync
        {
            private static readonly ILogger _log = Log.ForContext(typeof(Sync));
            
            public static async Task SyncData(string token)
            {
                await using var ctx = new ZenContext();
                var timestampSetting = await ctx.Settings.FindAsync(SettingCodes.SyncTimestamp);

                if (timestampSetting == null)
                {
                    timestampSetting = new Setting {Code = SettingCodes.SyncTimestamp, Value = "0"};
                    ctx.Settings.Add(timestampSetting);
                    await ctx.SaveChangesAsync();
                }

                long.TryParse(timestampSetting.Value, out long timestamp);

                _log.Information("Client timestamp: {0}", timestamp);
                
                await using var client = new ZenClient(token);
                var data = await client.Sync(timestamp);

                _log.Information("Updating local categories");
                await PullCategories(ctx, data.Tag);
                
                _log.Information("Updating local transactions");
                await PullTransactions(ctx, data.Transaction);

                timestampSetting.Value = data.ServerTimestamp.ToString();
                await ctx.SaveChangesAsync();
                
                _log.Information("Sync complete");
            }

            private static async Task PullCategories(ZenContext ctx, Tag[] tags)
            {
                foreach (var tag in tags)
                {
                    var dbTag = await ctx.Tags.FindAsync(tag.Id);
                    if (dbTag == null)
                    {
                        _log.Information("Creating category {0} ({1})", tag.Title, tag.Id);
                        dbTag = Mapper.Map<Tag>(tag);
                        await ctx.Tags.AddAsync(dbTag);
                    }
                    else
                    {
                        _log.Information("Updating category {0} ({1})", tag.Title, tag.Id);
                        Mapper.Map(tag, dbTag);
                    }
                }

                await ctx.SaveChangesAsync();
            }

            private static async Task PullTransactions(ZenContext ctx, Transaction[] transactions)
            {
                foreach (var transaction in transactions)
                {
                    var dbTrans = await ctx.Transactions.FindAsync(transaction.Id);
                    if (dbTrans == null)
                    {
                        _log.Information("Creating transaction {0}", transaction.Id);
                        dbTrans = Mapper.Map<Transaction>(transaction);
                        await ctx.Transactions.AddAsync(dbTrans);
                    }
                    else
                    {
                        _log.Information("Updating transaction {0}", transaction.Id);
                        Mapper.Map(transaction, dbTrans);
                    }
                    
                    if (dbTrans.TransactionTags == null)
                        dbTrans.TransactionTags = new List<TransactionTag>();

                    // Remove old tags
                    var removed = dbTrans.TransactionTags.Where(x => !transaction.Tag.Contains(x.Tag.Id)).ToArray();
                    foreach (var tag in removed)
                        dbTrans.TransactionTags.Remove(tag);
                    
                    // Add new tags
                    foreach (var tagId in transaction.Tag.EmptyIfNull())
                    {
                        if (!dbTrans.TransactionTags.Any(x => x.TagId == tagId))
                            dbTrans.TransactionTags.Add(new TransactionTag {TagId = tagId, TransactionId = dbTrans.Id});
                    }
                }
                
                _log.Information("Saving data");
                await ctx.SaveChangesAsync();
            }
        }
    }
}