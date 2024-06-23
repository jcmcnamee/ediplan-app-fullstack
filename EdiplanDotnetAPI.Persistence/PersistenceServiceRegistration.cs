using EdiplanDotnetAPI.Application.Contracts.Persistence;
using EdiplanDotnetAPI.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EdiplanDotnetAPI.Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<EdiplanDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString
                ("EdiplanDotnetAPIConnectionString"))
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors());

        services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));

        services.AddScoped<IBookingGroupRepository, BookingGroupRepository>();
        services.AddScoped<IBookingRepository, BookingRepository>();
        services.AddScoped<IAssetRepository, AssetRepository>();
        services.AddScoped<IEquipmentRepository, EquipmentRepository>();
        services.AddScoped<IPersonRepository, PersonRepository>();
        services.AddScoped<IRoomRepository, RoomRepository>();

        return services;
    }
}
