using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using ZenMoneyPlus.Data;
using ZenMoneyPlus.Data.Entities;
using ZenMoneyPlus.Helpers;
using ZenMoneyPlus.Web.Models;

namespace ZenMoneyPlus.Web.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class SpendingController : ControllerBase
{
    private readonly ILogger<SpendingController> _logger;
    private readonly ZenContext _context;

    public SpendingController(ZenContext context, ILogger<SpendingController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpPost]
    public async Task<SpendingResponse> GetSpendingData([FromBody] SpendingRequest request)
    {
        var data = await GetMonthData(request);
        return data;
    }

    private async Task<SpendingResponse> GetMonthData(SpendingRequest request)
    {
        var tags = await GetTagsWithChildren(request.Tags);
        var monthRange = request.MonthRange ?? throw new InvalidOperationException("Month range is not specified!");
        var startDate = new LocalDate(monthRange.From.Year, monthRange.From.Month, 1);
        var endDate = AdjsutEndDate(monthRange.To);

        var allTransactions = await _context.Transactions
                                            .Include(x => x.Tags)
                                            .ThenInclude(x => x.ParentTag)
                                            .Where(x => ((x.IncomeAccount != null && x.IncomeAccount.InBalance) ||
                                                         (x.OutcomeAccount != null && x.OutcomeAccount.InBalance)) &&
                                                        x.Income == 0 &&
                                                        x.Outcome > 0 &&
                                                        x.Tags.Any(t => tags.Contains(t.Id)) &&
                                                        x.Date >= startDate &&
                                                        x.Date < endDate)
                                            .ToArrayAsync();

        var grouped = new SortedDictionary<LocalDate, Dictionary<Tag, List<Transaction>>>();
        var tagTotals = new Dictionary<Tag, decimal>();
        foreach (var byMonth in allTransactions.GroupBy(x => (x.Date.Month, x.Date.Year)))
        {
            var byTag = new Dictionary<Tag, List<Transaction>>();
            foreach (var transaction in byMonth)
            {
                var tag = transaction.Tags.Count > 0
                              ? transaction.Tags[0]
                              : new Tag { Id = Guid.Empty.ToString(), Title = "<Unspecified>" };

                while (tag.ParentTag != null)
                    tag = tag.ParentTag;

                byTag.GetOrAdd(tag, () => new List<Transaction>()).Add(transaction);
                tagTotals.AddOrUpdate(tag, () => transaction.Amount, cur => cur + transaction.Amount);
            }

            grouped[new LocalDate(byMonth.Key.Year, byMonth.Key.Month, 1)] = byTag;
        }

        var sortedTags = tagTotals.OrderBy(x => x.Value).Select(x => x.Key).ToArray();
        var series = new List<SpendingSerie>();

        foreach (var tag in sortedTags)
        {
            var totals = new decimal[grouped.Count];
            foreach (var (month, i) in grouped.Enumerate())
            {
                if (!month.Value.TryGetValue(tag, out var transactions))
                    continue;

                totals[i] = transactions.Sum(x => x.Amount);
            }

            series.Add(new(tag.Title ?? "???", totals));
        }

        return new(grouped.Select(x => $"{x.Key.Month.ToString("D2")}.{x.Key.Year}").ToArray(), series.ToArray());
    }

    private async Task<string[]> GetTagsWithChildren(string[] tags)
    {
        var ids = new HashSet<string>();
        var queue = new Queue<Tag>();

        foreach (string id in tags)
        {
            var tag = await _context.Tags.FindAsync(id);
            if (tag == null)
            {
                _logger.LogWarning("Can't find tag with id {Id}", id);
                continue;
            }

            queue.Enqueue(tag);
        }

        while (queue.Count > 0)
        {
            var tag = queue.Dequeue();
            if (ids.Contains(tag.Id))
                continue;

            foreach (var child in tag.ChildrenTags)
                queue.Enqueue(child);

            ids.Add(tag.Id);
        }

        return ids.ToArray();
    }

    private LocalDate AdjsutEndDate(MonthAndYear endMonth)
    {
        (int month, int year) = endMonth;
        if (month == 12)
        {
            month = 1;
            year++;
        }
        else
        {
            month++;
        }

        return new LocalDate(year, month, 1);
    }
}