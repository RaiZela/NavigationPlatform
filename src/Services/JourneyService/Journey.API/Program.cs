using Journey.Infrastructure;
using Journey.Infrastructure.Data.Auth;
using Journey.Infrastructure.Data.Extensions;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddMassTransit(busConfigurator =>
{
    busConfigurator.SetKebabCaseEndpointNameFormatter();

    busConfigurator.UsingRabbitMq((context, configurator) =>
    {
        configurator.Host(new Uri(builder.Configuration["MessageBroker:Host"]!), h =>
        {
            h.Username(builder.Configuration["MessageBroker:Username"]);
            h.Password(builder.Configuration["MessageBroker:Password"]);
        });

        configurator.ConfigureEndpoints(context);
    });
});


//Add services to the container
builder.Services.AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices();

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .Enrich.WithEnvironmentName()
    .Enrich.WithThreadId()
    .Enrich.WithProcessId()
    .WriteTo.Async(wt => wt.Console(new Serilog.Formatting.Json.JsonFormatter()))
    .WriteTo.Async(wt => wt.File(new Serilog.Formatting.Json.JsonFormatter(), "Logs/logs.json"))
    .CreateLogger();

builder.Host.UseSerilog();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = "http://localhost:8080/realms/Nav-platform/";
                options.RequireHttpsMetadata = false;

                options.Audience = "account";
                var base64Key = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAxyjJiHBWyNX/iJ5EWJAMCAF/kxrsl8mZ8/EBR/pcYLGnBQV4OAIqcOhaE8H8H0Oy7SA7qL7j++oAh+kabsLezvtvsaXHsbFpwVtbuaVgGUPs0GRPFeEG/DW5a8zokbD8SmRfMuBcYoCqURqjEh+zYBIkxx8/5Quaxx/RDGDwLYJ0/roz2dPyPrf0jU7Nzaelq3WEMLhoxRk6Y3fwptngFqEEfa+dgVxwI1isx5vQb89QDrBBaOvM34fkV5f/pdLzybZebTGOKFoWZbdqQMH1biLUNGLxxC8C8pgznN+qtQ2bBIF/AhuvK1OnmedYSxqvJpnVuakuMAtKCkovnud5RwIDAQAB";

                // Convert to RSA key
                var rsa = RSA.Create();
                rsa.ImportRSAPublicKey(Convert.FromBase64String(base64Key), out _);

                // Create RsaSecurityKey
                var rsaSecurityKey = new RsaSecurityKey(rsa);

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidAudience = "account",
                    ValidateAudience = true,
                    ValidIssuer = "http://localhost:8080/realms/Nav-platform",
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = rsaSecurityKey,
                };

            });
builder.Services.AddAuthorization();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.UseApiServices();

app.UseMiddleware<CorrelationIdMiddleware>();
app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    await app.InitializeDatabaseAsync();
}
//Configure HTTP request pipeline

//app.MapGet("/", () => "Hello World!");

app.Run();
