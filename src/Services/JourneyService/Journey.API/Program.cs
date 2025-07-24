using Journey.Application.Helpers;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddHttpContextAccessor();


builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddHostedService<OutboxBackgroundService>();

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

builder.Services.AddMassTransitHostedService();

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(5100);
});

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


builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
            .AddJwtBearer(options =>
            {
                options.Authority = "http://localhost:8080/realms/Nav-platform/";
                options.RequireHttpsMetadata = false;
                var base64Key = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAxyjJiHBWyNX/iJ5EWJAMCAF/kxrsl8mZ8/EBR/pcYLGnBQV4OAIqcOhaE8H8H0Oy7SA7qL7j++oAh+kabsLezvtvsaXHsbFpwVtbuaVgGUPs0GRPFeEG/DW5a8zokbD8SmRfMuBcYoCqURqjEh+zYBIkxx8/5Quaxx/RDGDwLYJ0/roz2dPyPrf0jU7Nzaelq3WEMLhoxRk6Y3fwptngFqEEfa+dgVxwI1isx5vQb89QDrBBaOvM34fkV5f/pdLzybZebTGOKFoWZbdqQMH1biLUNGLxxC8C8pgznN+qtQ2bBIF/AhuvK1OnmedYSxqvJpnVuakuMAtKCkovnud5RwIDAQAB";

                // Convert to RSA key
                var rsa = RSA.Create();
                rsa.ImportSubjectPublicKeyInfo(Convert.FromBase64String(base64Key), out _);

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
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("authenticated", policy => policy.RequireAuthenticatedUser());
});


builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.All;
    logging.RequestHeaders.Add("Authorization");
    logging.ResponseHeaders.Add("Authorization");
    logging.RequestHeaders.Add("X-Request-ID");
    logging.ResponseHeaders.Add("X-Request-ID");
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseSerilogRequestLogging();

app.UseRouting();


app.UseAuthentication();


app.UseAuthorization();

app.UseApiServices();

app.UseMiddleware<CorrelationIdMiddleware>();
if (app.Environment.IsDevelopment())
{
    await app.InitializeDatabaseAsync();
}

app.UseHttpLogging();

app.Run();
