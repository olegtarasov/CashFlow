using ZenMoneyPlus.Data.Entities;
using ZenMoneyPlus.Models;

namespace ZenMoneyPlus.Messages;

internal record SyncRequest(long CurrentClientTimestamp, long ServerTimestamp, long CurrentClientTimezoneOffset = 180);

internal record SyncResponse(
    long ServerTimestamp,
    AccountModel[]? Account,
    TagModel[]? Tag,
    TransactionModel[]? Transaction);