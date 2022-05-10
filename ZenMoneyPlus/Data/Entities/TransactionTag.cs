using System.ComponentModel.DataAnnotations;

namespace ZenMoneyPlus.Data.Entities;

public class TransactionTag
{
    [MaxLength(100), Required]
    public string TagId { get; set; } = string.Empty;

    [MaxLength(100), Required]
    public string TransactionId { get; set; } = string.Empty;

    public virtual Tag? Tag { get; set; }
    public virtual Transaction? Transaction { get; set; }
}