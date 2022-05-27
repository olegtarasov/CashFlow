using System.Text.Json.Serialization;
using CashFlow.Helpers;
using NodaTime;

namespace CashFlow.Models;

internal record TagModel
{
    public string Id { get; init; } = string.Empty;
    public long User { get; init; }

    [JsonConverter(typeof(UnixSecondsToInstantConverter))]
    public Instant Changed { get; init; }

    public string? Icon { get; init; }
    public bool BudgetIncome { get; init; }
    public bool BudgetOutcome { get; init; }
    public bool? Required { get; init; }
    public string? Color { get; init; }
    public string? Picture { get; init; }
    public string? Title { get; init; }
    public bool ShowIncome { get; init; }
    public bool ShowOutcome { get; init; }
    public string? Parent { get; init; }
}