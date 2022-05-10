using ZenMoneyPlus.Data.Entities;

namespace ZenMoneyPlus.Messages;

public record SyncRequest(long CurrentClientTimestamp, long ServerTimestamp, long CurrentClientTimezoneOffset = 180);
public record SyncResponse(long ServerTimestamp, Tag[]? Tag, Transaction[]? Transaction);