using ZenMoneyPlus.Models;

namespace ZenMoneyPlus.Messages
{
    public class SyncResponse
    {
        public long ServerTimestamp { get; set; }
        public Tag[] Tag { get; set; }
        public Transaction[] Transaction { get; set; }
    }
}