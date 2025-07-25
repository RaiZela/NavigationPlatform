﻿namespace Journey.Domain.Outbox;

public sealed record OutboxMessage(
       Guid Id,
        string Name,
        string Content,
        DateTime CreatedOnUtc,
        DateTime? ProcessedOnUtc = null,
        string? Error = null)
{


}
