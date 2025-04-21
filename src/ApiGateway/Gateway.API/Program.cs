using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

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


builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = "http://localhost:18080/realms/Nav-platform";
                options.ClaimsIssuer = "http://localhost:18080/realms/Nav-platform";
                options.RequireHttpsMetadata = false;
                options.Audience = "account";
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = false
                };
            });
            



                builder.Services.AddAuthorization();

                var app = builder.Build();



                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

                app.UseHttpsRedirection();

                app.UseCors("CorsPolicy");

                app.UseAuthentication();

                app.UseAuthorization();

                //app.MapReverseProxy().RequireAuthorization();
                app.MapReverseProxy(proxyPipeline =>
                {
                    proxyPipeline.UseAuthentication();
                    proxyPipeline.UseAuthorization();
                });

                app.MapReverseProxy();

                app.Run();
        

