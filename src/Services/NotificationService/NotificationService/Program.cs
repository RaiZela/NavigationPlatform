using MassTransit;
using NotificationService.Hubs;
using NotificationService.JourneyEvents;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddHttpContextAccessor();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(busConfigurator =>
{
    busConfigurator.SetKebabCaseEndpointNameFormatter();

    busConfigurator.AddConsumers(typeof(Program).Assembly);
    //busConfigurator.AddConsumer<JourneyCreatedConsumer>();
    //busConfigurator.AddConsumer<JourneyUpdatedConsumer>();
    //busConfigurator.AddConsumer<JourneySharedConsumer>();

    busConfigurator.UsingRabbitMq((context, configurator) =>
    {
        configurator.Host(new Uri(builder.Configuration["MessageBroker:Host"]!), h =>
        {
            h.Username(builder.Configuration["MessageBroker:Username"]);
            h.Password(builder.Configuration["MessageBroker:Password"]);
        });
        //configurator.ReceiveEndpoint("notification-service", e =>
        //{
        //    e.ConfigureConsumer<JourneyCreatedConsumer>(context);
        //    e.ConfigureConsumer<JourneyUpdatedConsumer>(context);
        //    e.ConfigureConsumer<JourneySharedConsumer>(context);
        //});
        //configurator.ConfigureEndpoints(context);

        configurator.ReceiveEndpoint("journey-created", e =>
        {
            e.ConfigureConsumer<JourneyCreatedConsumer>(context);
        });

    });
});
builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.All;
    logging.RequestHeaders.Add("Authorization");
    logging.ResponseHeaders.Add("Authorization");
    logging.RequestHeaders.Add("X-Request-ID");
    logging.ResponseHeaders.Add("X-Request-ID");
});


Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .Enrich.WithEnvironmentName()
    .Enrich.WithThreadId()
    .Enrich.WithProcessId()
    .WriteTo.Async(wt => wt.Console(new Serilog.Formatting.Json.JsonFormatter()))
    .WriteTo.Async(wt => wt.File(new Serilog.Formatting.Json.JsonFormatter(), "Logs/logs.json"))
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddSignalR();
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(5200);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseRouting();

app.MapHub<NotificationHub>("/hub/notifications");

app.UseHttpLogging();

app.Run();


