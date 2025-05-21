namespace Journey.Infrastructure.Interceptors;

internal sealed class DispatchDomainEventsInterceptor(IMediator mediator) : SaveChangesInterceptor
{
    private static readonly JsonSerializerSettings SerializerSettings = new()
    {
        TypeNameHandling = TypeNameHandling.Auto
    };
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        if (eventData.Context is not null)
            DispatchDomainEvents(eventData.Context).GetAwaiter().GetResult();
        return base.SavingChanges(eventData, result);
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        if (eventData.Context is not null)
            await DispatchDomainEvents(eventData.Context);
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public async Task DispatchDomainEvents(DbContext? context)
    {

        if (context == null) return;

        var aggregates = context.ChangeTracker
            .Entries<IAggregate>()
            .Where(a => a.Entity.DomainEvents.Any())
            .Select(a => a.Entity);

        var domainEvents = aggregates
            .SelectMany(a => a.DomainEvents)
            .ToList();

        aggregates.ToList().ForEach(a => a.ClearDomainEvents());

        var messages = aggregates
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

        foreach (var domainEvent in domainEvents)
            await mediator.Publish(domainEvent);
    }
}
