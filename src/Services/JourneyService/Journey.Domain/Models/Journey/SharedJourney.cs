using Journey.Domain.Models.Auth;

namespace Journey.Domain.Models.Journey;

public class SharedJourney : Aggregate<Guid>
{
    public Guid OwnerId { get; set; }
    public User Owner { get; set; }
    public Guid SharedWIthId { get; set; }
    public User SharedWIth { get; set; }
    public Guid JourneyId { get; set; }
    public Journey Journey { get; set; }

    public static SharedJourney Create(Guid userId, Guid journeyId)
    {
        var shared = new SharedJourney
        {
            OwnerId = userId,
            JourneyId = journeyId
        };

        shared.AddDomainEvent(new JourneySharedEvent(shared));
        return shared;
    }

    public void Remove()
    {
        AddDomainEvent(new JourneyUnsharedEvent(Id));
    }
}


