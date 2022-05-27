using CashFlow.Models;
using CashFlow.Data.Entities;

namespace CashFlow.Messages;

internal record SyncRequest(long CurrentClientTimestamp, long ServerTimestamp, long CurrentClientTimezoneOffset = 180);

internal record SyncResponse(
    long ServerTimestamp,
    AccountModel[]? Account,
    TagModel[]? Tag,
    TransactionModel[]? Transaction);