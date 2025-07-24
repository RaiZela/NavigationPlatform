namespace Journey.Application.OutboxBackgroundService;

public sealed class OutboxProcessor(IApplicationDbContext dbContext, IPublishEndpoint publishEndpoint)
{
    private const int BatchSize = 10;
}
