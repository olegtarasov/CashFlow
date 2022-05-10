using System.ComponentModel.DataAnnotations;

namespace ZenMoneyPlus.Data.Entities;

public abstract class EntityBase
{
    [MaxLength(100), Key]
    public string Id { get; set; } = string.Empty;
    public long User { get; set; }
    public long Changed { get; set; }
}