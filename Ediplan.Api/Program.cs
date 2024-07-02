using Ediplan.Api;
using Serilog;

// Create new bootstrap logger with some default config - log to console only
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Ediplan API starting");

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog(
    (context, services, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services) // allow for use for services from apps DI container
    .Enrich.FromLogContext() // allow for contextual information to be logged from LogContext static class
    .WriteTo.Console(),
    true // dispose of logging sys on shutdown
    );

var app = builder
    .ConfigureServices()
    .ConfigurePipeline();

// Cleans up information about requests handled by ASP.NET Core and bundle into single event.
app.UseSerilogRequestLogging();

await app.ResetDatabaseAsync();

app.Run();

public partial class Program { }