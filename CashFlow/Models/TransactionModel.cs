using System.Text.Json.Serialization;
using CashFlow.Helpers;
using NodaTime;

namespace CashFlow.Models;

internal record TransactionModel
{
    public string Id { get; init; } = string.Empty;
    public long User { get; init; }
    public LocalDate Date { get; init; }

    [JsonConverter(typeof(UnixSecondsToInstantConverter))]
    public Instant Changed { get; init; }

    public decimal Income { get; init; }
    public decimal Outcome { get; init; }
    public long IncomeInstrument { get; init; }
    public long OutcomeInstrument { get; init; }

    [JsonConverter(typeof(UnixSecondsToInstantConverter))]
    public Instant Created { get; init; }

    public string? OriginalPayee { get; init; }
    public bool Deleted { get; init; }
    public bool Viewed { get; init; }
    public bool? Hold { get; init; }
    public string? QrCode { get; init; }
    public string? Source { get; init; }
    public string? IncomeAccount { get; init; }
    public string? OutcomeAccount { get; init; }
    public string[]? Tag { get; init; }
    public string? Comment { get; init; }
    public string? Payee { get; init; }
    public decimal? OpIncome { get; init; }
    public decimal? OpOutcome { get; init; }
    public string? OpIncomeInstrument { get; init; }
    public string? OpOutcomeInstrument { get; init; }
    public decimal? Latitude { get; init; }
    public decimal? Longitude { get; init; }
    public string? Merchant { get; init; }
    public string? IncomeBankId { get; init; }
    public string? OutcomeBankId { get; init; }
    public string? ReminderMarker { get; init; }
}