namespace Journey.Application.Journeys.Commands.ShareJourney;

public record SharedJourneyCommand(List<Guid> UsersIds, Guid JourneyId) : ICommand<SharedJourneyResult>;

public record SharedJourneyResult(bool IsSuccess, string Link, string? Message = "");

public class AddSharedJoourneyCommandValidator : AbstractValidator<SharedJourneyCommand>
{
    public AddSharedJoourneyCommandValidator()
    {
        RuleFor(x => x.UsersIds).NotEmpty().NotNull().WithMessage("Users are required!");
        RuleFor(x => x.JourneyId).NotEmpty().NotNull().WithMessage("Journey id required!");
    }
}

