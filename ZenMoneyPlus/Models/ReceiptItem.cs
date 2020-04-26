namespace ZenMoneyPlus.Models
{
    public class ReceiptItem
    {
        public long Id { get; set; }
        public decimal? Sum { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public decimal? Quantity { get; set; }

        public long ReceiptId { get; set; }
        public virtual Receipt Receipt { get; set; }
    }
}