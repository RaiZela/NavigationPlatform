namespace Journey.Application.Data;

public interface IApplicationDbContext
{
    DbSet<JourneyEntity> Journeys { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
