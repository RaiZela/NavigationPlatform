namespace Journey.Domain.Abstractions;

public abstract class Entity<T> : IEntity<T>
{
    public T Id { get; set; }
    public DateTime? CreatedAt { get; set; }
    public Guid CreatedByUserId { get; set; } // FK
    public User CreatedByUser { get; set; }
    public DateTime? LastModified { get; set; }
    public Guid LastModifiedByUserId { get; set; }
    public User LastModifiedByUser { get; set; }
}
