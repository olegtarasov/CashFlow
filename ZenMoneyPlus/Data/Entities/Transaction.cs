using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using NodaTime;

namespace ZenMoneyPlus.Data.Entities;

public enum TransactionType
{
    Income,
    Outcome,
    Transfer
}

public class Transaction : EntityBase
{
    public LocalDate Date { get; init; }
    public decimal Income { get; set; }
    public decimal Outcome { get; set; }
    public long IncomeInstrument { get; set; }
    public long OutcomeInstrument { get; set; }
    public Instant Created { get; set; }
    public string? OriginalPayee { get; set; }
    public bool Deleted { get; set; }
    public bool Viewed { get; set; }
    public bool? Hold { get; set; }
    public string? QrCode { get; set; }
    public string? Comment { get; set; }
    public string? Payee { get; set; }
    public decimal? OpIncome { get; set; }
    public decimal? OpOutcome { get; set; }
    public string? OpIncomeInstrument { get; set; }
    public string? OpOutcomeInstrument { get; set; }
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
    public string? Merchant { get; set; }
    public string? IncomeBankId { get; set; }
    public string? OutcomeBankId { get; set; }
    public string? ReminderMarker { get; set; }
    public bool GetReceiptFailed { get; set; }

    public virtual List<Tag> Tags { get; set; } = new();
    public virtual Receipt? Receipt { get; set; }
    public virtual Account? IncomeAccount { get; set; }
    public virtual Account? OutcomeAccount { get; set; }

    [NotMapped]
    public decimal Amount => Income != 0 ? Income : Outcome;

    [NotMapped]
    public bool IsInBalance => IncomeAccount?.InBalance == true || OutcomeAccount?.InBalance == true;

    [NotMapped]
    public TransactionType Type => (Income != 0 && Outcome != 0)
                                       ? TransactionType.Transfer
                                       : Income == 0
                                           ? TransactionType.Outcome
                                           : TransactionType.Income;

    public override string ToString()
    {
        string tags = Tags.Count > 0
                          ? Tags.Where(x => x.Title != null).Select(x => x.Title!).Aggregate((a, b) => a + ", " + b)
                          : "";

        return $"[{Type.ToString()[0]}] {Date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)} [{tags}] {Amount}";
    }
}