using System.ComponentModel.DataAnnotations;
using NodaTime;

namespace CashFlow.Data.Entities;

/// <summary>
/// Account type.
/// </summary>
public enum AccountType
{
    /// <summary>
    /// Credit card.
    /// </summary>
    Ccard,

    /// <summary>
    /// Cash.
    /// </summary>
    Cash,

    /// <summary>
    /// Deposit.
    /// </summary>
    Deposit,

    /// <summary>
    /// Debt.
    /// </summary>
    Debt
}

/// <summary>
/// Account (bank account, credit card or simply a wallet with cash).
/// </summary>
public class Account : EntityBase
{
    /// <summary>
    /// Instrument (as in currency).
    /// </summary>
    public int Instrument { get; set; }

    /// <summary>
    /// Account type.
    /// </summary>
    public AccountType Type { get; set; }

    /// <summary>
    /// Role.
    /// </summary>
    public string? Role { get; set; }

    /// <summary>
    /// Indicates whether account is private.
    /// </summary>
    public bool Private { get; set; }

    /// <summary>
    /// Indicates whether account is a savings account.
    /// </summary>
    public bool Savings { get; set; }

    /// <summary>
    /// Title.
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Indicates whether account participates in balance calculations.
    /// </summary>
    public bool InBalance { get; set; }

    /// <summary>
    /// Credit limit.
    /// </summary>
    public decimal CreditLimit { get; set; }

    /// <summary>
    /// Start balance.
    /// </summary>
    public decimal StartBalance { get; set; }

    /// <summary>
    /// Current balance.
    /// </summary>
    public decimal Balance { get; set; }

    /// <summary>
    /// Company.
    /// </summary>
    public string? Company { get; set; }

    /// <summary>
    /// Indicates whether account is archived.
    /// </summary>
    public bool Archive { get; set; }

    /// <summary>
    /// ???
    /// </summary>
    public bool EnableCorrection { get; set; }

    /// <summary>
    /// ???
    /// </summary>
    public string? BalanceCorrectionType { get; set; }

    /// <summary>
    /// Start date.
    /// </summary>
    public LocalDate? StartDate { get; set; }

    /// <summary>
    /// ???
    /// </summary>
    public bool? Capitalization { get; set; }

    /// <summary>
    /// ???
    /// </summary>
    public decimal? Percent { get; set; }

    /// <summary>
    /// ???
    /// </summary>
    public string? SyncId { get; set; }

    /// <summary>
    /// ???
    /// </summary>
    public bool EnableSms { get; set; }

    /// <summary>
    /// ???
    /// </summary>
    public int? EndDateOffset { get; set; }

    /// <summary>
    /// ???
    /// </summary>
    public string? EndDateOffsetInterval { get; set; }

    /// <summary>
    /// ???
    /// </summary>
    public int? PayoffStep { get; set; }

    /// <summary>
    /// ???
    /// </summary>
    public string? PayoffInterval { get; set; }
}