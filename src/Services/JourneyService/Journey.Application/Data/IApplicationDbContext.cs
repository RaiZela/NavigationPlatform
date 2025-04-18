using Journey.Domain.Models;

namespace Journey.Application.Data;

public interface IApplicationDbContext
{
    DbSet<JourneyEntity> Journeys { get; }
    DbSet<User> Users { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
