namespace Journey.Infrastructure.Interceptors;

internal sealed class InsertOutboxMessagesInterceptor : SaveChangesInterceptor
{
    private static readonly JsonSerializerSettings SerializerSettings = new()
    {
        TypeNameHandling = TypeNameHandling.Auto
    };
    public async override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {

        if (eventData.Context is not null)
        {
            InsertOutboxMessages(eventData.Context);
        }

        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }


    private static void InsertOutboxMessages(DbContext context)
    {
        var domainEvents = context
            .ChangeTracker
            .Entries<IAggregate>()
            .SelectMany(e => e.Entity.DomainEvents)
            .Where(e => e is IOutboxCapableEvent)
            .ToList();

        context.ChangeTracker
             .Entries<IAggregate>()
             .ToList()
             .ForEach(entry => entry.Entity.ClearDomainEvents());

        var messages = domainEvents
            .Select(domainEvent =>
            {
                var outboxMessage = new OutboxMessage
                    (Guid.NewGuid(),
                    domainEvent.GetType().Name,
                    JsonConvert.SerializeObject(domainEvent, SerializerSettings),
                    DateTime.UtcNow)
                ;
                return outboxMessage;
            })
            .ToList();

        context.Set<OutboxMessage>().AddRange(messages);

    }
}
