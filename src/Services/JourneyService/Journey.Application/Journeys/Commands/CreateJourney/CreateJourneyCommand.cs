using BuildingBlocks.CQRS;
using FluentValidation;

namespace Journey.Application.Journeys.Commands.CreateJourney;

public record CreateJourneyCommand(JourneyDto Journey)
    : ICommand<CreateJourneyResult>;


public record CreateJourneyResult(Guid Id);

public class CreateJourneyCommandValidator : AbstractValidator<CreateJourneyCommand>
{
    public CreateJourneyCommandValidator()
    {
        RuleFor(x => x.Journey.ArrivalLocation).NotEmpty().NotNull().MinimumLength(2).MaximumLength(20)
            .WithMessage("Destination is required!");

        RuleFor(x => x.Journey.StartLocation).NotEmpty().NotNull().MinimumLength(2).MaximumLength(20)
            .WithMessage("Start location is required!");

        RuleFor(x => x.Journey.DistanceKm.Value).LessThan(1000).WithMessage("Distance must be less than 1000!");

        RuleFor(x => x.Journey.StartTime).NotNull().NotEmpty().WithMessage("Start time is required!");

        RuleFor(x => x.Journey.ArrivalTime).NotNull().NotEmpty().WithMessage("Arrival time is required!");
    }
}