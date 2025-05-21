namespace BuildingBlocks.Middleware.Exceptions;

public class ValidationExceptionMiddleware : IMiddleware
{
    private readonly ILogger<ValidationExceptionMiddleware> _logger;

    public ValidationExceptionMiddleware(ILogger<ValidationExceptionMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (ValidationException ex)
        {
            _logger.LogWarning("Validation failed: {Errors}", ex.Errors);

            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/json";

            var errors = ex.Errors.Select(e => new
            {
                field = e.PropertyName,
                error = e.ErrorMessage
            });

            await context.Response.WriteAsJsonAsync(new { errors });
        }
    }
}

