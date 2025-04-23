namespace Journey.Infrastructure.Data.Interceptors
{
    public class AuditableEntityInterceptor : SaveChangesInterceptor
    {
        private readonly ICurrentUserService _currentUserService;
        private bool _isSavingChanges = false;  // To guard against recursive SaveChanges
        public AuditableEntityInterceptor(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            if (_isSavingChanges)
                return base.SavingChanges(eventData, result);

            try
            {
                _isSavingChanges = true;
                UpdateEntities(eventData.Context);
                return base.SavingChanges(eventData, result);
            }
            catch (Exception ex)
            {
                // TODO: add logging here
                throw;
            }
            finally
            {
                _isSavingChanges = false;
            }
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            if (_isSavingChanges)
                return base.SavingChangesAsync(eventData, result, cancellationToken);

            try
            {
                _isSavingChanges = true;
                UpdateEntities(eventData.Context);
                return base.SavingChangesAsync(eventData, result, cancellationToken);
            }
            catch (Exception ex)
            {
                // TODO: add logging here
                throw;
            }
            finally
            {
                _isSavingChanges = false;
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
                    throw new ArgumentNullException("Username is null");

                var user = context.Set<User>().FirstOrDefault(u => u.Username == username);
                if (user == null)
                {
                    user = new User
                    {
                        Id = Guid.NewGuid(),
                        Username = username
                    };

                    // Avoid calling SaveChanges here to prevent recursion.
                    // You can handle adding the user after all changes are saved.
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
}
