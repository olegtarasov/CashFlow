using System.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ZenMoneyPlus.Clients;
using ZenMoneyPlus.Data;
using ZenMoneyPlus.Data.Entities;
using ZenMoneyPlus.Helpers;
using ZenMoneyPlus.Models;

namespace ZenMoneyPlus.Services;

public class ZenService
{
    private readonly ILogger<ZenService> _log;
    private readonly ZenContext _context;
    private readonly ZenClient _client;
    private readonly IMapper _mapper;


    public ZenService(ZenContext context, ILogger<ZenService> log, ZenClient client, IMapper mapper)
    {
        _context = context;
        _log = log;
        _client = client;
        _mapper = mapper;
    }

    public async Task Sync()
    {
        long timestamp = await GetLocalTimestamp();
        _log.LogInformation("Local timestamp: {Timestamp}", timestamp);

        var data = await _client.Sync(timestamp);

        if (data.Tag != null && data.Tag.Length > 0)
        {
            _log.LogInformation("Updating local categories");
            await PullCategories(data.Tag);
        }

        if (data.Account != null && data.Account.Length > 0)
        {
            _log.LogInformation("Updating local accounts");
            await PullAccounts(data.Account);
        }

        if (data.Transaction != null && data.Transaction.Length > 0)
        {
            _log.LogInformation("Updating local transactions");
            await PullTransactions(data.Transaction);
        }

        await SetLocalTimestamp(data.ServerTimestamp);

        _log.LogInformation("Sync complete");
    }

    public async Task GetMissingReceipts(bool retryFailed = false)
    {
        var transactions = await _context.Transactions
                                         .Where(x => x.QrCode != null &&
                                                     x.QrCode.Length > 0 &&
                                                     x.Receipt == null &&
                                                     x.GetReceiptFailed == retryFailed)
                                         .ToArrayAsync();

        for (int i = 0; i < transactions.Length; i++)
        {
            _log.LogInformation("Getting {Current} out of {Total} missing receipts", i + 1, transactions.Length);
            var receipt = await _client.GetReceipt(transactions[i].QrCode!);
            if (receipt == null)
            {
                _log.LogWarning($"Failed to get a receipt for transaction {transactions[i]}");
                transactions[i].GetReceiptFailed = true;
                continue;
            }

            var dbReceipt = _mapper.Map<Receipt>(receipt);
            await _context.Receipts.AddAsync(dbReceipt);
            await _context.ReceiptItems.AddRangeAsync(dbReceipt.Items);

            transactions[i].Receipt = dbReceipt;

            await _context.SaveChangesAsync();
        }
    }

    private async Task PullAccounts(AccountModel[] accounts)
    {
        foreach (var account in accounts)
        {
            var dbAccount = await _context.Accounts.FindAsync(account.Id);
            if (dbAccount == null)
            {
                dbAccount = _mapper.Map<Account>(account);
                await _context.Accounts.AddAsync(dbAccount);
            }
            else
            {
                _mapper.Map(account, dbAccount);
            }

            _log.LogInformation("Processing account {AccountId}", account.Id);
        }

        await _context.SaveChangesAsync();
    }

    private async Task PullCategories(TagModel[] tags)
    {
        foreach (var tag in tags)
        {
            var dbTag = await _context.Tags.FindAsync(tag.Id);
            if (dbTag == null)
            {
                _log.LogInformation("Creating category {Title} ({Id})", tag.Title, tag.Id);
                dbTag = _mapper.Map<Tag>(tag);
                await _context.Tags.AddAsync(dbTag);
            }
            else
            {
                _log.LogInformation("Updating category {Title} ({Id})", tag.Title, tag.Id);
                _mapper.Map(tag, dbTag);
            }
        }

        await _context.SaveChangesAsync();
    }

    private async Task PullTransactions(TransactionModel[] transactions)
    {
        foreach (var transaction in transactions)
        {
            var dbTrans = await _context.Transactions.FindAsync(transaction.Id);
            if (dbTrans == null)
            {
                dbTrans = _mapper.Map<Transaction>(transaction);
                await _context.Transactions.AddAsync(dbTrans);
            }
            else
            {
                _mapper.Map(transaction, dbTrans);
            }

            // Remove old tags
            if (transaction.Tag == null || transaction.Tag.Length == 0)
            {
                dbTrans.Tags.Clear();
            }
            else
            {
                dbTrans.Tags.RemoveAll(x => !transaction.Tag.Contains(x.Id));
            }

            // Add new tags
            foreach (var tagId in transaction.Tag.EmptyIfNull())
            {
                if (!dbTrans.Tags.Any(x => x.Id == tagId))
                {
                    dbTrans.Tags.Add(await FindExpectedEntity<Tag>(tagId));
                }
            }

            // Link accounts
            if (!string.IsNullOrEmpty(transaction.IncomeAccount))
            {
                dbTrans.IncomeAccount = await FindExpectedEntity<Account>(transaction.IncomeAccount);
            }

            if (!string.IsNullOrEmpty(transaction.OutcomeAccount))
            {
                dbTrans.OutcomeAccount = await FindExpectedEntity<Account>(transaction.OutcomeAccount);
            }

            _log.LogInformation("Processing transaction {Transaction}", dbTrans);
        }

        _log.LogInformation("Saving data");
        await _context.SaveChangesAsync();
    }

    private async Task<long> GetLocalTimestamp()
    {
        var timestampSetting = await _context.Settings.FindAsync(SettingCodes.SyncTimestamp);

        if (timestampSetting == null)
        {
            timestampSetting = new Setting { Code = SettingCodes.SyncTimestamp, Value = "0" };
            await _context.Settings.AddAsync(timestampSetting);
            await _context.SaveChangesAsync();
        }

        long.TryParse(timestampSetting.Value, out long timestamp);

        return timestamp;
    }

    private async Task SetLocalTimestamp(long timestamp)
    {
        var timestampSetting = await _context.Settings.FindAsync(SettingCodes.SyncTimestamp);

        if (timestampSetting == null)
        {
            timestampSetting = new Setting { Code = SettingCodes.SyncTimestamp };
            await _context.Settings.AddAsync(timestampSetting);
        }

        timestampSetting.Value = timestamp.ToString();

        await _context.SaveChangesAsync();
    }

    private async Task<T> FindExpectedEntity<T>(string key) where T : class
    {
        var entity = await _context.Set<T>().FindAsync(key);
        if (entity == null)
        {
            throw new DataException($"Failed to find an expected entity {typeof(T).Name} with key {key}");
        }

        return entity;
    }
}