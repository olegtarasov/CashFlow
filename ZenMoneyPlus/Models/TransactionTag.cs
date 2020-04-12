using System.ComponentModel.DataAnnotations;

namespace ZenMoneyPlus.Models
{
    public class TransactionTag
    {
        [MaxLength(100)]
        public string TagId { get; set; }

        [MaxLength(100)]
        public string TransactionId { get; set; }

        public Tag Tag { get; set; }
        public Transaction Transaction { get; set; }
    }
}