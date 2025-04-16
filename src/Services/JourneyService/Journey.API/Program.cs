using Journey.API;
using Journey.Application;
using Journey.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container
builder.Services.AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices();

var app = builder.Build();

//Configure HTTP request pipeline

//app.MapGet("/", () => "Hello World!");

app.Run();
