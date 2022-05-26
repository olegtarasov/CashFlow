using System.Text.Json.Serialization;
using NodaTime;
using ZenMoneyPlus.Data.Entities;
using ZenMoneyPlus.Helpers;

namespace ZenMoneyPlus.Models;

internal record AccountModel
{
    public string Id { get; init; } = string.Empty;
    public long User { get; init; }

    [JsonConverter(typeof(UnixSecondsToInstantConverter))]
    public Instant Changed { get; init; }

    public int Instrument { get; init; }
    public AccountType Type { get; init; }
    public string? Role { get; init; }
    public bool Private { get; init; }
    public bool Savings { get; init; }
    public string? Title { get; init; }
    public bool InBalance { get; init; }
    public decimal CreditLimit { get; init; }
    public decimal StartBalance { get; init; }
    public decimal Balance { get; init; }
    public string? Company { get; init; }
    public bool Archive { get; init; }
    public bool EnableCorrection { get; init; }
    public string? BalanceCorrectionType { get; init; }
    public LocalDate? StartDate { get; init; }
    public bool? Capitalization { get; init; }
    public decimal? Percent { get; init; }
    public string? SyncId { get; init; }
    public bool EnableSms { get; init; }
    public int? EndDateOffset { get; init; }
    public string? EndDateOffsetInterval { get; init; }
    public int? PayoffStep { get; init; }
    public string? PayoffInterval { get; init; }
}