using BuildingBlocks.Middleware.Exceptions;

namespace Journey.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddCarter();
        services.AddScoped<ValidationExceptionMiddleware>();

        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
        app.MapCarter();
        app.UseMiddleware<ValidationExceptionMiddleware>();
        return app;
    }
}
