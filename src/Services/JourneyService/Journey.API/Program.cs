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

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = "https://your-auth0-domain/";
        options.Audience = "https://your-api";
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
