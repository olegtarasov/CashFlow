using System.ComponentModel.DataAnnotations;

namespace ZenMoneyPlus.Models
{
    public class Setting
    {
        [MaxLength(100), Key]
        public string Code { get; set; }

        public string Value { get; set; }
    }
}