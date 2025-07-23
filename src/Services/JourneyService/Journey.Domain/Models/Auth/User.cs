using System.ComponentModel.DataAnnotations.Schema;

namespace Journey.Domain.Models.Auth;

public class User
{
    public Guid Id { get; set; }
    public string? Email { get; set; }
    public string? Role { get; set; }
    public string Username { get; set; }

    [InverseProperty(nameof(FavoriteJourney.ActionUser))]
    public virtual ICollection<FavoriteJourney> FavouriteJourneys { get; set; }
    public virtual ICollection<SharedJourney> SharedJourneys { get; set; }

}
