namespace Journey.Application.Journeys.Commands.UpdateJourney;

public record UpdateJourneyCommand(JourneyDto Journey)
    : ICommand<UpdateJourneyResult>;


public record UpdateJourneyResult(bool isSuccess);

public class UpdateJourneyCommandValidator : AbstractValidator<UpdateJourneyCommand>
{
    public UpdateJourneyCommandValidator()
    {
        RuleFor(x => x.Journey.ArrivalLocation).NotEmpty().NotNull().MinimumLength(2).MaximumLength(20)
            .WithMessage("Destination is required!");

        RuleFor(x => x.Journey.StartLocation).NotEmpty().NotNull().MinimumLength(2).MaximumLength(20)
            .WithMessage("Start location is required!");

        RuleFor(x => x.Journey.DistanceKm).LessThan(1000).WithMessage("Distance must be less than 1000!");

        RuleFor(x => x.Journey.StartTime).NotNull().NotEmpty().WithMessage("Start time is required!");

        RuleFor(x => x.Journey.ArrivalTime).NotNull().NotEmpty().WithMessage("Arrival time is required!");
    }
}