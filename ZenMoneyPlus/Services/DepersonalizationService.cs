using System.Reflection;
using Microsoft.Extensions.Logging;
using ZenMoneyPlus.Data;
using ZenMoneyPlus.Helpers;

namespace ZenMoneyPlus.Services;

public class DepersonalizationService
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
        }

        await _context.SaveChangesAsync();
    }

    private async Task DepersonalizeReceiptItems()
    {
        _logger.LogInformation("Depersonalizing receipt items");

        foreach (var item in _context.ReceiptItems)
        {
            item.Name = ReplaceToken(item.Name);
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

        foreach (var item in _context.Transactions)
        {
            item.OriginalPayee = ReplaceToken(item.OriginalPayee);
            item.QrCode = null;
            item.Comment = ReplaceToken(item.Comment);
            item.Payee = ReplaceToken(item.Payee);
            item.Latitude = null;
            item.Longitude = null;
            item.Merchant = ReplaceToken(item.Merchant);
            item.ReminderMarker = null;
            item.User = ReplaceId(item.User);
        }

        await _context.SaveChangesAsync();
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