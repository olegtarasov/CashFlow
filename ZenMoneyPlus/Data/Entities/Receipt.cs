using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZenMoneyPlus.Data.Entities;

public class Receipt
{
    public long Id { get; set; }
    public decimal? Sum { get; set; }
    public string? Date { get; set; }
    public string? Time { get; set; }
    public string? Payee { get; set; }
    public string? Address { get; set; }
    public decimal? CardSum { get; set; }
    public decimal? CashSum { get; set; }
        
    [NotMapped] 
    public ReceiptItem[]? Items { get; set; }

    public virtual List<ReceiptItem> ReceiptItems { get; set; } = new();

    [MaxLength(100), Required(AllowEmptyStrings = false)]
    public string TransactionId { get; set; } = string.Empty;
    public virtual Transaction? Transaction { get; set; }
}