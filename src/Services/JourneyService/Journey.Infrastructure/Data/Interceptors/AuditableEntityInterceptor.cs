using Journey.Infrastructure.Data.Auth;

namespace Journey.Infrastructure.Data.Interceptors;

public class AuditableEntityInterceptor : SaveChangesInterceptor
{
    private readonly ICurrentUserService _currentUserService;
    public AuditableEntityInterceptor(ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
    }
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
            var username = _currentUserService.Username;

            if (string.IsNullOrEmpty(username))
                return;

            var user = context.Set<User>().FirstOrDefault(u => u.Username == username);
            if (user == null)
            {
                user = new User
                {
                    Id = Guid.NewGuid(),
                    Username = username
                };

                context.Set<User>().Add(user);
                context.SaveChanges();
            }

            var userId = user.Id;

            foreach (var entry in context.ChangeTracker.Entries<IEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedByUserId = userId;
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                }

                if (entry.State == EntityState.Added || entry.State == EntityState.Modified || entry.HasChangedOwnedEntities())
                {
                    entry.Entity.LastModifiedByUserId = userId;
                    entry.Entity.LastModified = DateTime.UtcNow;
                }
            }
        }
        catch (Exception ex)
        {
            // TODO: add logging here
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
