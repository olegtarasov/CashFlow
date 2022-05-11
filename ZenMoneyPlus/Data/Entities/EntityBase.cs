using System.ComponentModel.DataAnnotations;
using NodaTime;

namespace ZenMoneyPlus.Data.Entities;

public abstract class EntityBase
{
    [MaxLength(100), Key]
    public string Id { get; set; } = string.Empty;
    public long User { get; set; }
    public Instant Changed { get; set; }
}