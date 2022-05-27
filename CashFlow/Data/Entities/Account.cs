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
    /// Account type.
    /// </summary>
    public AccountType Type { get; set; }

    /// <summary>
    /// Title.
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Indicates whether account participates in balance calculations.
    /// </summary>
    public bool InBalance { get; set; }
}