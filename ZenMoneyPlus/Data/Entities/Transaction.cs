using System.ComponentModel.DataAnnotations.Schema;

namespace ZenMoneyPlus.Data.Entities;

public class Transaction : EntityBase
{
    public decimal? Income { get; set; }
    public decimal? Outcome { get; set; }
    public long IncomeInstrument { get; set; }
    public long OutcomeInstrument { get; set; }
    public long Created { get; set; }
    public string? OriginalPayee { get; set; }
    public bool Deleted { get; set; }
    public bool Viewed { get; set; }
    public bool? Hold { get; set; }
    public string? QrCode { get; set; }
    public string? IncomeAccount { get; set; }
    public string? OutcomeAccount { get; set; }
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

    [NotMapped]
    public string[]? Tag { get; set; }

    public virtual List<TransactionTag> TransactionTags { get; set; } = new();
    public virtual Receipt? Receipt { get; set; }
}