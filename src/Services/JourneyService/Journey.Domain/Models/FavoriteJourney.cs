namespace Journey.Domain.Models;

public class FavoriteJourney
{
    public Guid JourneyId { get; set; }
    public Journey Journey { get; set; } 
    public Guid UserId { get; set; }
    public User User { get; set; }
}
