using BuildingBlocks.Extensions.Authentication;
using Journey.Infrastructure;
using Journey.Infrastructure.Data.Auth;
using Journey.Infrastructure.Data.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;
using static System.Net.WebRequestMethods;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();


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
                options.Authority = "http://localhost:18080/realms/Nav-platform";
                options.RequireHttpsMetadata = false;
                options.Audience = "account";
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidIssuer= "http://localhost:18080/realms/Nav-platform",
                    ValidateIssuer = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = false
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
