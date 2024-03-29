using System.ComponentModel.DataAnnotations;
using NodaTime;

namespace CashFlow.Data.Entities;

/// <summary>
/// Base entity.
/// </summary>
public abstract class EntityBase
{
    /// <summary>
    /// Unique entity id.
    /// </summary>
    [MaxLength(100), Key]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// User id that owns this instance.
    /// </summary>
    public long User { get; set; }

    /// <summary>
    /// An instant that entity was last changed at.
    /// </summary>
    public Instant Changed { get; set; }
}