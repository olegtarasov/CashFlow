using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using NodaTime;

namespace CashFlow.Data.Entities;

/// <summary>
/// Transaction type.
/// </summary>
public enum TransactionType
{
    /// <summary>
    /// Income.
    /// </summary>
    Income,

    /// <summary>
    /// Outcome.
    /// </summary>
    Outcome,

    /// <summary>
    /// Transfer.
    /// </summary>
    Transfer
}

/// <summary>
/// Transaction.
/// </summary>
public class Transaction : EntityBase
{
    /// <summary>
    /// Date.
    /// </summary>
    public LocalDate Date { get; init; }

    /// <summary>
    /// Income amount.
    /// </summary>
    public decimal Income { get; set; }

    /// <summary>
    /// Outcome amount.
    /// </summary>
    public decimal Outcome { get; set; }

    /// <summary>
    /// Creation date and time.
    /// </summary>
    public Instant Created { get; set; }

    /// <summary>
    /// Indicates whether this transaction is deleted.
    /// </summary>
    public bool Deleted { get; set; }

    /// <summary>
    /// Indicates whether this transaction is viewed.
    /// </summary>
    public bool Viewed { get; set; }

    /// <summary>
    /// Payee.
    /// </summary>
    public string? Payee { get; set; }

    /// <summary>
    /// Transaction tags.
    /// </summary>
    public virtual List<Tag> Tags { get; set; } = new();

    /// <summary>
    /// Receipt.
    /// </summary>
    public virtual Receipt? Receipt { get; set; }

    /// <summary>
    /// Income account.
    /// </summary>
    public virtual Account? IncomeAccount { get; set; }

    /// <summary>
    /// Outcome account.
    /// </summary>
    public virtual Account? OutcomeAccount { get; set; }

    /// <summary>
    /// Amount spent or received (chooses <see cref="Income"/> or <see cref="Outcome"/>).
    /// </summary>
    [NotMapped]
    public decimal Amount => Income != 0 ? Income : Outcome;

    /// <summary>
    /// Indicated either one of associated accounts is in balance.
    /// </summary>
    [NotMapped]
    public bool IsInBalance => IncomeAccount?.InBalance == true || OutcomeAccount?.InBalance == true;

    /// <summary>
    /// Calculates transaction type based on values in <see cref="Income"/> and <see cref="Outcome"/>.
    /// </summary>
    [NotMapped]
    public TransactionType Type => (Income != 0 && Outcome != 0)
                                       ? TransactionType.Transfer
                                       : Income == 0
                                           ? TransactionType.Outcome
                                           : TransactionType.Income;

    /// <inheritdoc />
    public override string ToString()
    {
        string tags = Tags.Count > 0
                          ? Tags.Select(x => x.Title).Aggregate((a, b) => a + ", " + b)
                          : "";

        return $"[{Type.ToString()[0]}] {Date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)} [{tags}] {Amount}";
    }
}