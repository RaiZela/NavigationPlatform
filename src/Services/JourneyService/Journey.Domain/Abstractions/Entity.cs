using Journey.Domain.Models.Auth;
using System.ComponentModel.DataAnnotations.Schema;

namespace Journey.Domain.Abstractions;

public abstract class Entity<T> : IEntity<T>
{
    public T Id { get; set; }
    public DateTime? CreatedAt { get; set; }

    [ForeignKey(nameof(CreatedByUser.Id))]
    public Guid CreatedByUserId { get; set; }
    public User CreatedByUser { get; set; }
    public DateTime? LastModified { get; set; }

    [ForeignKey(nameof(LastModifiedByUser.Id))]
    public Guid LastModifiedByUserId { get; set; }
    public User LastModifiedByUser { get; set; }
}
