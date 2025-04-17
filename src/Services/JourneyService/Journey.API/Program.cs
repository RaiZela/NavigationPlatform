using Journey.API;
using Journey.Application;
using Journey.Infrastructure;
using Journey.Infrastructure.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container
builder.Services.AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices();

var app = builder.Build();

app.UseApiServices();

if (app.Environment.IsDevelopment())
{
    await app.InitializeDatabaseAsync();
}
//Configure HTTP request pipeline

//app.MapGet("/", () => "Hello World!");

app.Run();
