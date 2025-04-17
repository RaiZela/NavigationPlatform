using BuildingBlocks.CQRS;
using FluentValidation;
using Journey.Application.Dtos;

namespace Journey.Application.Journeys.Commands.CreateOrder;

public record CreateJourneyCommand(JourneyDto journey)
    : ICommand<CreateJourneyResult>;


public record CreateJourneyResult(Guid Id);

public class CreateJourneyCommandValidator : AbstractValidator<CreateJourneyCommand>
{
    public CreateJourneyCommandValidator()
    {
        RuleFor(x => x.journey.ArrivalLocation).NotEmpty().NotNull().MinimumLength(2).MaximumLength(20)
            .WithMessage("Destination is required!");

        RuleFor(x => x.journey.StartLocation).NotEmpty().NotNull().MinimumLength(2).MaximumLength(20)
            .WithMessage("Start location is required!");

        RuleFor(x => x.journey.DistanceKm.value).LessThan(1000).WithMessage("Distance location is required!");

        RuleFor(x => x.journey.StartTime).NotNull().NotEmpty().WithMessage("Start time is required!");

        RuleFor(x => x.journey.ArrivalTime).NotNull().NotEmpty().WithMessage("Arrival time is required!");
    }
}