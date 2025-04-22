using MassTransit;
using Microsoft.AspNetCore.SignalR;
using NotificationService.Events;
using NotificationService.Hubs;

namespace NotificationService.JourneyEvents;

public class JourneySharedConsumer : IConsumer<JourneySharedEvent>
{
    private readonly ILogger<JourneySharedConsumer> _logger;
    private readonly IHubContext<NotificationHub> _hub;

    public JourneySharedConsumer(ILogger<JourneySharedConsumer> logger, IHubContext<NotificationHub> hub)
    {
        _logger = logger;
        _hub = hub;
    }

    public async Task Consume(ConsumeContext<JourneySharedEvent> context)
    {
        var e = context.Message;
        _logger.LogInformation("Journey Shared: {JourneyId}", e.Journey.Id);

        await _hub.Clients.All.SendAsync("JourneyShared", new
        {
            e.Journey.Id,
            e.Journey.CreatedByUser,
            Message = $"Journey '{e.Journey.ArrivalLocation}' was shared!"
        });
    }
}
