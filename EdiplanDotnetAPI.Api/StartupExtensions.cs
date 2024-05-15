using EdiplanDotnetAPI.Api.Middleware;
using EdiplanDotnetAPI.Api.Services;
using EdiplanDotnetAPI.Application;
using EdiplanDotnetAPI.Application.Contracts;
using EdiplanDotnetAPI.Infrastructure;
using EdiplanDotnetAPI.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EdiplanDotnetAPI.Api;

public static class StartupExtensions
{

    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddApplicationServices();
        builder.Services.AddInfrastructureServices(builder.Configuration);
        builder.Services.AddPersistenceServices(builder.Configuration);
        //builder.Services.AddIdentityServices(builder.Configuration);

        builder.Services.AddScoped<ILoggedInUserService, LoggedInUserService>();
        builder.Services.AddHttpContextAccessor();

        builder.Services.AddControllers();

        builder.Services.AddCors(
            options => options.AddPolicy(
                "open",
                policy => policy.WithOrigins([builder.Configuration["ApiUrl"] ??
                "https://localhost:7205",
                builder.Configuration["ClientUrl"] ??
                "https://localhost:7080"])
                .AllowAnyMethod()
                .SetIsOriginAllowed(pol => true)
                .AllowAnyHeader()
                .AllowCredentials()));

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        //app.MapIdentityApi<ApplicationUser>();

        app.UseCors("open");

        if(app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCustomExceptionHandler();

        app.UseHttpsRedirection();
        app.MapControllers();

        return app;
    }

    public static async Task ResetDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        try
        {
            var context = scope.ServiceProvider.GetService<EdiplanDbContext>();
            if (context != null)
            {
                await context.Database.EnsureDeletedAsync();
                await context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            // Add logging
            
        }
    }
}
