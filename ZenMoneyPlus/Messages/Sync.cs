using ZenMoneyPlus.Data.Entities;
using ZenMoneyPlus.Models;

namespace ZenMoneyPlus.Messages;

public record SyncRequest(long CurrentClientTimestamp, long ServerTimestamp, long CurrentClientTimezoneOffset = 180);
public record SyncResponse(long ServerTimestamp, AccountModel[]? Account, TagModel[]? Tag, TransactionModel[]? Transaction);