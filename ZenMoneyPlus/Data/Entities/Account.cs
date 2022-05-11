using System.ComponentModel.DataAnnotations;
using NodaTime;

namespace ZenMoneyPlus.Data.Entities;

public enum AccountType
{
    Ccard,
    Cash,
    Deposit,
    Debt
}

public class Account : EntityBase
{
    public int Instrument { get; set; }
    public AccountType Type { get; set; }
    public string? Role { get; set; }
    public bool Private { get; set; }
    public bool Savings { get; set; }
    public string? Title { get; set; }
    public bool InBalance { get; set; }
    public decimal CreditLimit { get; set; }
    public decimal StartBalance { get; set; }
    public decimal Balance { get; set; }
    public string? Company { get; set; }
    public bool Archive { get; set; }
    public bool EnableCorrection { get; set; }
    public string? BalanceCorrectionType { get; set; }
    public LocalDate? StartDate { get; set; }
    public bool? Capitalization { get; set; }
    public decimal? Percent { get; set; }
    public string? SyncId { get; set; }
    public bool EnableSms { get; set; }
    public int? EndDateOffset { get; set; }
    public string? EndDateOffsetInterval { get; set; }
    public int? PayoffStep { get; set; }
    public string? PayoffInterval { get; set; }
}