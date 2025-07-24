using Microsoft.Extensions.Hosting;

namespace Journey.Application.Helpers;

public class OutboxBackgroundService(IServiceScopeFactory serviceScopeFactory,
    ILogger<OutboxBackgroundService> logger) : BackgroundService
{
    private const int OutboxCheckInterval = 7;
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            logger.LogInformation("Starting OutBoxBackgroundService...");

            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = serviceScopeFactory.CreateScope();
                var outboxService = scope.ServiceProvider.GetRequiredService<OutboxProcessor>();

                await outboxService.Execute(stoppingToken);

                await Task.Delay(TimeSpan.FromSeconds(OutboxCheckInterval), stoppingToken);
            }

        }
        catch (OperationCanceledException)
        {
            logger.LogInformation("OutBoxBackgroundService has been cancelled.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred in OutBoxBackgroundService.");
        }
        finally
        {
            logger.LogInformation("OutBoxBackgroundService has stopped.");
        }
    }
}
