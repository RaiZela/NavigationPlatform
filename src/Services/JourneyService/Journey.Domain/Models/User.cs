namespace Journey.Domain.Models;

public class User
{
    public Guid Id { get; set; }
    public string? Email { get; set; }
    public string? Role { get; set; }
    public string Username { get; set; }
    public virtual ICollection<Journey> CreatedJourneys { get; set; }
    public virtual ICollection<FavoriteJourney> FavouriteJourneys { get; set; }
    public virtual ICollection<SharedJourney> SharedJourneys { get; set; }
}
