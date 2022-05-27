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
    /// Icon.
    /// </summary>
    public string? Icon { get; set; }

    /// <summary>
    /// ???
    /// </summary>
    public bool BudgetIncome { get; set; }

    /// <summary>
    /// ???
    /// </summary>
    public bool BudgetOutcome { get; set; }

    /// <summary>
    /// ???
    /// </summary>
    public bool Required { get; set; }

    /// <summary>
    /// Color.
    /// </summary>
    public string? Color { get; set; }

    /// <summary>
    /// Picture.
    /// </summary>
    public string? Picture { get; set; }

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