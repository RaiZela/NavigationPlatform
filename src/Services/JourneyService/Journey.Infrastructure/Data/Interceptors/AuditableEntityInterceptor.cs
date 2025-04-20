using Journey.Infrastructure.Data.Auth;

namespace Journey.Infrastructure.Data.Interceptors;

public class AuditableEntityInterceptor : SaveChangesInterceptor
{
    //private readonly ICurrentUserService _currentUserService;
    //public AuditableEntityInterceptor(ICurrentUserService currentUserService)
    //{
    //    _currentUserService = currentUserService;
    //}
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        try
        {
            UpdateEntities(eventData.Context);
            return base.SavingChanges(eventData, result);
        }
        catch (Exception ex)
        {

            throw;
        }

    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        try
        {
            UpdateEntities(eventData.Context);
            return base.SavingChangesAsync(eventData, result, cancellationToken);

        }
        catch (Exception ex)
        {

            throw;
        }
    }

    public void UpdateEntities(DbContext? context)
    {
        if (context == null)
            return;
        try
        {
            foreach (var entry in context.ChangeTracker.Entries<IEntity>())
            {
                //var userId = _currentUserService.UserId ?? "system";

                if (entry.State == EntityState.Added)
                {
                    // entry.Entity.CreatedByUserId = Guid.Parse(userId);
                    entry.Entity.CreatedByUserId = Guid.Parse("b82d3a63-0bfb-4c2d-9c29-f2d9c9535f6c\r\n");
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                }

                if (entry.State == EntityState.Added || entry.State == EntityState.Modified || entry.HasChangedOwnedEntities())
                {
                    //entry.Entity.LastModifiedByUserId = Guid.Parse(userId);
                    entry.Entity.LastModifiedByUserId = Guid.Parse("b82d3a63-0bfb-4c2d-9c29-f2d9c9535f6c\r\n");
                    entry.Entity.LastModified = DateTime.UtcNow;
                }
            }

        }
        catch (Exception ex)
        {

            throw;
        }
    }
}

public static class Extensions
{
    public static bool HasChangedOwnedEntities(this EntityEntry entry) =>
        entry.References.Any(r =>
        r.TargetEntry != null &&
        r.TargetEntry.Metadata.IsOwned() &&
        (r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified));
}
