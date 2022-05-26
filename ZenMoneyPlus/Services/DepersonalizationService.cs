using System.Reflection;
using Microsoft.Extensions.Logging;
using ZenMoneyPlus.Data;
using ZenMoneyPlus.Data.Entities;
using ZenMoneyPlus.Helpers;

namespace ZenMoneyPlus.Services;

internal class DepersonalizationService
{
    private readonly ILogger<DepersonalizationService> _logger;
    private readonly ZenContext _context;
    private readonly string[] _nouns;
    private readonly Dictionary<string, string> _tokens = new();
    private readonly Dictionary<long, long> _ids = new();
    private readonly Random _rnd = new();

    public DepersonalizationService(ZenContext context, ILogger<DepersonalizationService> logger)
    {
        _logger = logger;
        _context = context;

        var accessor = new ResourceAccessor(Assembly.GetExecutingAssembly());
        _nouns = accessor.String("Resources.nouns.txt").Split("\n");
    }

    public async Task Depersolaize()
    {
        await DepersonalizeAccounts();
        await DepersonalizeReceiptItems();
        await DepersonalizeReceipts();
        await DepersonalizeTags();
        await DepersonalizeTransactions();
    }

    private async Task DepersonalizeAccounts()
    {
        _logger.LogInformation("Depersonalizing accounts");

        foreach (var account in _context.Accounts)
        {
            account.Title = ReplaceToken(account.Title);
            account.User = ReplaceId(account.User);
            account.StartBalance = 0;
            account.Balance = 0;
        }

        await _context.SaveChangesAsync();
    }

    private async Task DepersonalizeReceiptItems()
    {
        _logger.LogInformation("Depersonalizing receipt items");

        foreach (var item in _context.ReceiptItems)
        {
            item.Name = ReplaceToken(item.Name);
            item.Price = MutateMoney(item.Price);
            item.Sum = Math.Round(item.Price * item.Quantity, 2);
        }

        await _context.SaveChangesAsync();
    }

    private async Task DepersonalizeReceipts()
    {
        _logger.LogInformation("Depersonalizing receipts");

        foreach (var item in _context.Receipts)
        {
            item.Payee = ReplaceToken(item.Payee);
            item.Address = ReplaceToken(item.Address);

            if (item.Inn != null)
            {
                item.Inn = long.TryParse(item.Inn, out long inn) ? ReplaceId(inn).ToString() : null;
            }

            if (item.Sum != null)
            {
                item.Sum = item.Items.Count > 0
                               ? Math.Round(item.Items.Sum(x => x.Sum), 2)
                               : MutateMoney(item.Sum.Value);
            }

            if (item.CardSum != null)
            {
                item.CardSum = item.Sum;
            }

            item.CashSum = null;
        }

        await _context.SaveChangesAsync();
    }

    private async Task DepersonalizeTags()
    {
        _logger.LogInformation("Depersonalizing tags");

        foreach (var item in _context.Tags)
        {
            item.Icon = ReplaceToken(item.Icon);
            item.Picture = ReplaceToken(item.Picture);
            item.Title = ReplaceToken(item.Title);
            item.User = ReplaceId(item.User);
        }

        await _context.SaveChangesAsync();
    }

    private async Task DepersonalizeTransactions()
    {
        _logger.LogInformation("Depersonalizing transactions");

        var removed = new List<Transaction>();

        foreach (var item in _context.Transactions)
        {
            if (item.Income > 0)
            {
                removed.Add(item);
                continue;
            }

            item.OriginalPayee = ReplaceToken(item.OriginalPayee);
            item.QrCode = null;
            item.Comment = ReplaceToken(item.Comment);
            item.Payee = ReplaceToken(item.Payee);
            item.Latitude = null;
            item.Longitude = null;
            item.Merchant = ReplaceToken(item.Merchant);
            item.ReminderMarker = null;
            item.User = ReplaceId(item.User);

            if (item.Outcome > 0)
            {
                if (item.Receipt != null && item.Receipt.Items.Count > 0)
                {
                    item.Outcome = Math.Round(item.Receipt.Items.Sum(x => x.Sum), 2);
                }
                else
                {
                    item.Outcome = MutateMoney(item.Outcome);
                }
            }

            item.OpIncome = item.OpOutcome = null;
        }

        _context.RemoveRange(removed.Where(x => x.Receipt != null).SelectMany(x => x.Receipt.Items));
        _context.RemoveRange(removed.Where(x => x.Receipt != null).Select(x => x.Receipt));
        _context.RemoveRange(removed);

        await _context.SaveChangesAsync();
    }

    private decimal MutateMoney(decimal amount)
    {
        decimal mult = amount < 20_000
                           ? (decimal)_rnd.NextDouble()
                           : (decimal)(_rnd.NextDouble() * (0.05 - 0.01) + 0.01);
        return Math.Round(amount * mult, 2);
    }

    private long ReplaceId(long id) => _ids.GetOrAdd(id, () => _rnd.NextInt64());

    private string? ReplaceToken(string? token)
    {
        if (string.IsNullOrEmpty(token))
            return token;

        if (!_tokens.TryGetValue(token, out var replacement))
        {
            int num1 = _rnd.Next(_nouns.Length);
            int num2 = _rnd.Next(_nouns.Length);
            int num3 = _rnd.Next(_nouns.Length);
            replacement = $"{_nouns[num1]} {_nouns[num2]} {_nouns[num3]}";
            _tokens[token] = replacement;
        }

        return replacement;
    }
}