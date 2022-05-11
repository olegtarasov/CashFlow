using System.ComponentModel.DataAnnotations;
using NodaTime;

namespace ZenMoneyPlus.Data.Entities;

public class Receipt
{
    public long Id { get; set; }
    public decimal? Sum { get; set; }
    public LocalDate Date { get; set; }
    public LocalTime Time { get; set; }
    public string? Payee { get; set; }
    public string? Address { get; set; }
    public decimal? CardSum { get; set; }
    public decimal? CashSum { get; set; }
    public string? Inn { get; set; }
        
    public virtual List<ReceiptItem> Items { get; set; } = new();

    [MaxLength(100), Required(AllowEmptyStrings = false)]
    public string TransactionId { get; set; } = string.Empty;
    public virtual Transaction? Transaction { get; set; }
}