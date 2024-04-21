using System.Data.Common;
using EdiplanDotnetAPI.Domain.Common;
using EdiplanDotnetAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;


namespace EdiplanDotnetAPI.Persistence;
public class EdiplanDbContext : DbContext
{
    public EdiplanDbContext(DbContextOptions<EdiplanDbContext> options)
        : base(options)
    {
    }

    public DbSet<Booking> Bookings { get; set; }
    public DbSet<BookingGroup> BookingGroups { get; set; }
    public DbSet<Production> Productions { get; set; }
    public DbSet<Location> Locations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Apply configurations (we do not want to litter our entities with attributes)
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EdiplanDbContext).Assembly);

        // Seed booking Groups
        var bg1 = Guid.Parse("b5b7a148-0452-4e0e-8298-7c37fd4caa64");
        var bg2 = Guid.Parse("4658345f-4454-4128-a2f8-c673e73fa846");
        var bg3 = Guid.Parse("146023a2-4255-4cfd-893a-d04b0839e616");
        var bg4 = Guid.Parse("1cd16d2e-a35b-48ef-ab93-debd26d445f0");

        modelBuilder.Entity<BookingGroup>().HasData(new BookingGroup
        {
            Id = bg1,
            Name = "Offline"
        });
        modelBuilder.Entity<BookingGroup>().HasData(new BookingGroup
        {
            Id = bg2,
            Name = "Online"
        });
        modelBuilder.Entity<BookingGroup>().HasData(new BookingGroup
        {
            Id = bg3,
            Name = "Dub - In House"
        });
        modelBuilder.Entity<BookingGroup>().HasData(new BookingGroup
        {
            Id = bg4,
            Name = "Grade"
        });

        // Seed Productions
        var prog1 = Guid.Parse("4050a623-5308-4640-8c36-493729f6f884");
        var prog2 = Guid.Parse("709bf680-7cc8-406c-bb8d-13ace00d4fe7");
        var prog3 = Guid.Parse("3cbedfd3-a8b1-43b2-9ccb-67ec980118a6");
        var prog4 = Guid.Parse("d7af2c8c-525e-41ad-b379-edad3de1defe");

        modelBuilder.Entity<Production>().HasData(new Production 
        {
            Id = prog1,
            Name = "Teen Mom UK Series 10",
        });
        modelBuilder.Entity<Production>().HasData(new Production 
        {
            Id = prog2,
            Name = "The Great British Bake Off!",
        });
        modelBuilder.Entity<Production>().HasData(new Production 
        {
            Id = prog3,
            Name = "Top Gear Special",
        });
        modelBuilder.Entity<Production>().HasData(new Production 
        {
            Id = prog4,
            Name = "The Crown Season 5",
        });

        // Seed locations
        var location1 = Guid.Parse("e19d79c7-58d6-4906-ba7a-3507a2e90f09");
        var location2 = Guid.Parse("71e40a55-2430-4a68-8adc-f78a1ef2c8c2");
        var location3 = Guid.Parse("5e10152d-dd1b-49a2-bc95-79246ee8ca8a");
        var location4 = Guid.Parse("189d7685-bdf0-4a39-9750-7720ec6044c9");

        modelBuilder.Entity<Location>().HasData(new Location 
        {
            Id = location1,
            Name = "True North Productions, Leeds",
        });
        modelBuilder.Entity<Location>().HasData(new Location 
        {
            Id = location2,
            Name = "Picture Shop, Manchester",
        });
        modelBuilder.Entity<Location>().HasData(new Location 
        {
            Id = location3,
            Name = "The Crown Production Office, London",
        });
        modelBuilder.Entity<Location>().HasData(new Location 
        {
            Id = location4,
            Name = "Top Gear Production Office, London",
        });

        // Seed Bookings
        modelBuilder.Entity<Booking>().HasData(new Booking
        {
            Id = Guid.NewGuid(),
            StartDate = DateTime.Now.AddDays(2),
            EndDate = DateTime.Now.AddDays(23),
            ProductionId = prog1,
            LocationId = location1,
            IsConfirmed = false,
            Notes = "High-speed internet required for remote editing."
        });
        modelBuilder.Entity<Booking>().HasData(new Booking
        {
            Id = Guid.NewGuid(),
            StartDate = DateTime.Now.AddMonths(1),
            EndDate = DateTime.Now.AddMonths(1).AddDays(7),
            ProductionId = location2,
            IsConfirmed = false,
            Notes = "Need access to soundproof dubbing studio."
        });
        modelBuilder.Entity<Booking>().HasData(new Booking
        {
            Id = Guid.NewGuid(),
            StartDate = DateTime.Now.AddDays(-15),
            EndDate = DateTime.Now.AddDays(-10),
            ProductionId = prog2,
            LocationId = location2,
            IsConfirmed = true,
            Notes = "Final editing phase."
        });
        modelBuilder.Entity<Booking>().HasData(new Booking
        {
            Id = Guid.NewGuid(),
            StartDate = DateTime.Now.AddMonths(-2),
            EndDate = DateTime.Now.AddDays(-5),
            ProductionId = prog3,
            LocationId = location4,
            IsConfirmed = true,
            Notes = "Location scouting."
        });
        modelBuilder.Entity<Booking>().HasData(new Booking
        {
            Id = Guid.NewGuid(),
            StartDate = DateTime.Now.AddMonths(-3),
            EndDate = DateTime.Now.AddMonths(-2).AddDays(-10),
            ProductionId = prog4,
            LocationId = location3,
            IsConfirmed = true,
            Notes = "Principal photography."
        });
        modelBuilder.Entity<Booking>().HasData(new Booking
        {
            Id = Guid.NewGuid(),
            StartDate = DateTime.Now.AddMonths(2),
            EndDate = DateTime.Now.AddMonths(2).AddDays(5),
            ProductionId = prog3,
            LocationId = location4,
            IsConfirmed = false,
            Notes = "Pre-production meetings."
        });
    }

    // Extra code to update the tracking properties on the AuditableEntity
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedDate = DateTime.Now;
                    break;
                case EntityState.Modified:
                    entry.Entity.LastModifiedDate = DateTime.Now;
                    break;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}
