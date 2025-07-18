using Microsoft.AspNetCore.Mvc;

namespace Journey.API.Endpoints;

public record CreateJourneyRequest(JourneyDto Journey);
public record CreateJourneyResponse(Guid Id);

public class CreateJourney : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {

        app.MapPost("/journeys", async ([FromBody] CreateJourneyRequest request, ISender sender) =>
        {
            if (request.Journey == null)
            {
                return Results.BadRequest("Request body is null");
            }

            var command = request.Adapt<CreateJourneyCommand>();
            var result = await sender.Send(command);

            var response = result.Adapt<CreateJourneyResponse>();

            return Results.Created($"/journeys/{response.Id}", response);
        })
            .WithName("CreateJourney")
            .Produces<CreateJourneyResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Journey")
            .WithDescription("Create Journey")
            .RequireAuthorization("authenticated");
    }
}

