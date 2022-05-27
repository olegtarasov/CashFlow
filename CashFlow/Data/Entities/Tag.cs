using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using NodaTime;

namespace CashFlow.Data.Entities;

/// <summary>
/// Transaction tag.
/// </summary>
public class Tag : EntityBase
{
    /// <summary>
    /// Title.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Indicates whether this tag is valid for income transactions (not enforced).
    /// </summary>
    public bool ShowIncome { get; set; }

    /// <summary>
    /// Indicates whether this tag is valid for outcome transactions (not enforced).
    /// </summary>
    public bool ShowOutcome { get; set; }

    /// <summary>
    /// Parent tag id.
    /// </summary>
    public string? Parent { get; set; }

    /// <summary>
    /// Parent tag.
    /// </summary>
    [JsonIgnore]
    public virtual Tag? ParentTag { get; set; }

    /// <summary>
    /// Children tags.
    /// </summary>
    public virtual List<Tag> ChildrenTags { get; set; } = new();

    /// <summary>
    /// Transactions tagged with this tag.
    /// </summary>
    [JsonIgnore]
    public virtual List<Transaction> Transactions { get; set; } = new();
}