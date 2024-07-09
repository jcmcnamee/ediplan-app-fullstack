using Ediplan.Api.Middleware;
using Ediplan.Api.Services;
using Ediplan.Application;
using Ediplan.Application.Contracts;
using Ediplan.Infrastructure;
using Ediplan.Persistence;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;

namespace Ediplan.Api;

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

        builder.Services.AddControllers(options =>
        {
            options.ReturnHttpNotAcceptable = true;
        }).AddNewtonsoftJson(setupAction =>
        {
            setupAction.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            setupAction.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            setupAction.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;
            setupAction.SerializerSettings.DateParseHandling = Newtonsoft.Json.DateParseHandling.DateTimeOffset;
        })
        .AddXmlDataContractSerializerFormatters();

        builder.Services.AddCors(
            options => options.AddPolicy(
                "open",
                policy => policy.WithOrigins([builder.Configuration["ApiUrl"] ??
                "https://localhost:7080",
                builder.Configuration["ClientUrl"] ??
                "https://localhost:5173"])
                .AllowAnyMethod()
                .SetIsOriginAllowed(pol => true)
                .AllowAnyHeader()
                .AllowCredentials()
                .WithExposedHeaders("X-Pagination")
                ));

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
