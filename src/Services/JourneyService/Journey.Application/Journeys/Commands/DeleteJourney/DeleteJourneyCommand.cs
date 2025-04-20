using FluentValidation;

namespace Journey.Application.Journeys.Commands.DeleteJourney;

public record DeleteJourneyCommand(Guid Id)
    : ICommand<DeleteJourneyResult>;


public record DeleteJourneyResult(bool isSuccess);

public class DeleteJourneyCommandValidator : AbstractValidator<DeleteJourneyCommand>
{
    public DeleteJourneyCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull()
            .WithMessage("ID is required!");

    }
}