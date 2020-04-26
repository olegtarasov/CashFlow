using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZenMoneyPlus.Models
{
    public class Receipt
    {
        public long Id { get; set; }
        public decimal? Sum { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Payee { get; set; }
        public string Address { get; set; }
        public decimal? CardSum { get; set; }
        public decimal? CashSum { get; set; }
        
        [NotMapped] 
        public ReceiptItem[] Items { get; set; }

        public virtual List<ReceiptItem> ReceiptItems { get; set; }

        [MaxLength(100), Required]
        public string TransactionId { get; set; }
        public virtual Transaction Transaction { get; set; }
    }
}