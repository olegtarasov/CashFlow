using System.ComponentModel.DataAnnotations;

namespace ZenMoneyPlus.Models
{
    public abstract class EntityBase
    {
        [MaxLength(100), Key]
        public string Id { get; set; }
        public long User { get; set; }
        public long Changed { get; set; }
    }
}