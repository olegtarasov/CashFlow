using System.ComponentModel.DataAnnotations;
using NodaTime;

namespace CashFlow.Data.Entities;

/// <summary>
/// Spending receipt.
/// </summary>
public class Receipt
{
    /// <summary>
    /// Id.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Total amount of money spent.
    /// </summary>
    public decimal Sum { get; set; }

    /// <summary>
    /// Date.
    /// </summary>
    public LocalDate Date { get; set; }

    /// <summary>
    /// Time.
    /// </summary>
    public LocalTime Time { get; set; }

    /// <summary>
    /// Payee.
    /// </summary>
    public string? Payee { get; set; }

    /// <summary>
    /// Venue address.
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// Amount paid with card.
    /// </summary>
    public decimal? CardSum { get; set; }

    /// <summary>
    /// Amount paid with cash.
    /// </summary>
    public decimal? CashSum { get; set; }

    /// <summary>
    /// Payee INN.
    /// </summary>
    public string? Inn { get; set; }

    /// <summary>
    /// Receipt items.
    /// </summary>
    public virtual List<ReceiptItem> Items { get; set; } = new();

    /// <summary>
    /// Parent transaction id.
    /// </summary>
    [MaxLength(100), Required(AllowEmptyStrings = false)]
    public string TransactionId { get; set; } = string.Empty;

    /// <summary>
    /// Parent transaction.
    /// </summary>
    public virtual Transaction? Transaction { get; set; }
}