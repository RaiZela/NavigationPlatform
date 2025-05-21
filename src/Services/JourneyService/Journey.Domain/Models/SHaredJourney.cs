namespace Journey.Domain.Models;

public class SharedJourney
{
    public Guid OwnerId { get; set; }
    public User Owner { get; set; }
    public Guid SharedWIthId { get; set; }
    public User SharedWIth { get; set; }
    public Guid JourneyId { get; set; }
    public Journey Journey { get; set; }
}
