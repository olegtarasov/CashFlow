using System.ComponentModel.DataAnnotations;

namespace ZenMoneyPlus.Models
{
    public class TransactionTag
    {
        [MaxLength(100)]
        public string TagId { get; set; }

        [MaxLength(100)]
        public string TransactionId { get; set; }

        public virtual Tag Tag { get; set; }
        public virtual Transaction Transaction { get; set; }
    }
}