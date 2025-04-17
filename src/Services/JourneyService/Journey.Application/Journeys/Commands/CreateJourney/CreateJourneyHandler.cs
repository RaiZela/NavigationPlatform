using BuildingBlocks.CQRS;
using Journey.Application.Journeys.Commands.CreateOrder;

namespace Journey.Application.Journeys.Commands.CreateJourney;

public class CreateJourneyHandler
    : ICommandHandler<CreateJourneyCommand, CreateJourneyResult>
{
    public Task<CreateJourneyResult> Handle(CreateJourneyCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
