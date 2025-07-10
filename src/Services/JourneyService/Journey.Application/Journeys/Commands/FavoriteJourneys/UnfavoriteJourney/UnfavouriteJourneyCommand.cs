namespace Journey.Application.Journeys.Commands.FavoriteJourneys.UnfavoriteJourney;

public record UnfavouriteJourneyCommand(Guid Id) : ICommand<UnfavouriteJourneyResult>;
public record UnfavouriteJourneyResult(bool IsSuccess);
public class RemoveFavoriteJourneyCommandValidator : AbstractValidator<UnfavouriteJourneyCommand>
{
    public RemoveFavoriteJourneyCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull().WithMessage("Journey id is required!");
    }
}
