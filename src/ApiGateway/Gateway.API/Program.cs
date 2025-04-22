using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Security.Cryptography;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(policy =>
{
    policy.AddPolicy("CorsPolicy", opt => opt
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5000);
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

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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




builder.Services.AddAuthorization();

builder.Services.AddHttpLogging(logging => {
    logging.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.All;
});

var app = builder.Build();



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseSerilogRequestLogging();

app.UseCors("CorsPolicy");

app.UseAuthentication();

app.UseAuthorization();


//app.MapReverseProxy().RequireAuthorization();
app.MapReverseProxy(proxyPipeline =>
{
    //proxyPipeline.UseAuthentication();
    //proxyPipeline.UseAuthorization();
});
app.UseHttpLogging();

app.Run();


