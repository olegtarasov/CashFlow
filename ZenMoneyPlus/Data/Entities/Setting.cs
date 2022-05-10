using System.ComponentModel.DataAnnotations;

namespace ZenMoneyPlus.Data.Entities;

public class Setting
{
    [MaxLength(100), Key]
    public string Code { get; set; } = string.Empty;

    [Required(AllowEmptyStrings = false)]
    public string Value { get; set; } = string.Empty;
}