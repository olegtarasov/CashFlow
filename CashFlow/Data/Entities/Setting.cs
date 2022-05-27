using System.ComponentModel.DataAnnotations;

namespace CashFlow.Data.Entities;

/// <summary>
/// Setting.
/// </summary>
public class Setting
{
    /// <summary>
    /// Code.
    /// </summary>
    [MaxLength(100), Key]
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Value.
    /// </summary>
    [Required(AllowEmptyStrings = false)]
    public string Value { get; set; } = string.Empty;
}