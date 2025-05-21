namespace Journey.Application.Journeys.Commands.FavoriteJourneys.AddFavoriteJourney;

public record FavoritedJourneyCommand(Guid Id) : ICommand<AddFavoriteJourneyResult>;

public record AddFavoriteJourneyResult(bool isSuccess);

public class AddFavoriteJourneyCommandValidator : AbstractValidator<FavoritedJourneyCommand>
{
    public AddFavoriteJourneyCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull().WithMessage("Journey id is required!");
    }
}