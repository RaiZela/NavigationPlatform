using MassTransit;
using Microsoft.AspNetCore.SignalR;
using NotificationService.Events;
using NotificationService.Hubs;

namespace NotificationService.JourneyEvents;

public class JourneyUpdatedConsumer : IConsumer<JourneyUpdatedEvent>
{
    private readonly ILogger<JourneyUpdatedConsumer> _logger;
    private readonly IHubContext<NotificationHub> _hub;

    public JourneyUpdatedConsumer(ILogger<JourneyUpdatedConsumer> logger, IHubContext<NotificationHub> hub)
    {
        _logger = logger;
        _hub = hub;
    }

    public async Task Consume(ConsumeContext<JourneyUpdatedEvent> context)
    {
        var e = context.Message;
        _logger.LogInformation("Journey shared: {JourneyId}", e.Journey.Id);

        await _hub.Clients.All.SendAsync("JourneyCreated", new
        {
            e.Journey.Id,
            e.Journey.CreatedByUser,
            Message = $"Journey '{e.Journey.ArrivalLocation}' was shared!"
        });
    }
}
