using System.Data.Common;
using EdiplanDotnetAPI.Application.Contracts;
using EdiplanDotnetAPI.Domain.Common;
using EdiplanDotnetAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;


namespace EdiplanDotnetAPI.Persistence;
public class EdiplanDbContext : DbContext
{
    private readonly ILoggedInUserService? _loggedInUserService;

    public EdiplanDbContext(DbContextOptions<EdiplanDbContext> options)
        : base(options)
    {
    }

    public EdiplanDbContext(DbContextOptions<EdiplanDbContext> options,
        ILoggedInUserService loggedInUserService)
        : base(options)
    {
        _loggedInUserService = loggedInUserService;
    }

    public DbSet<Booking> Bookings { get; set; }
    public DbSet<BookingGroup> BookingGroups { get; set; }
    public DbSet<Production> Productions { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Asset> Assets { get; set; }
    public DbSet<Person> Persons { get; set; }
    public DbSet<Equipment> Equipment { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<AssetGroup> AssetGroups { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Apply configurations (we do not want to litter our entities with attributes)
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EdiplanDbContext).Assembly);


        // Define TPC strategy and use a sequence to ensure continuity of Id field between tables.
        modelBuilder.HasSequence<int>("AssetIds");
        modelBuilder.Entity<Asset>()
            .UseTpcMappingStrategy()
            .Property(a => a.Id)
            //.HasDefaultValueSql("nextval('AssetIds'::regclass)") ; // Db dependant language, change for Db type.
            .HasDefaultValueSql("nextval('\"AssetIds\"')");

        modelBuilder.Entity<Equipment>()
            .HasBaseType<Asset>()
            .ToTable("equipment");

        modelBuilder.Entity<Person>()
            .HasBaseType<Asset>()
            .ToTable("person");

        modelBuilder.Entity<Room>()
            .HasBaseType<Asset>()
            .ToTable("room");

        modelBuilder.Entity<Asset>()
            .HasMany(a => a.AssetGroups)
            .WithMany(a => a.Assets)
            .UsingEntity("asset_group_map");

        modelBuilder.Entity<Booking>()
            .HasMany(b => b.BookingGroups)
            .WithMany(b => b.Bookings)
            .UsingEntity("booking_group_map");

        modelBuilder.Entity<Booking>()
            .ToTable("booking");

        modelBuilder.Entity<Production>()
            .ToTable("production");

        modelBuilder.Entity<AssetGroup>()
            .ToTable("asset_group");

        modelBuilder.Entity<BookingGroup>()
            .ToTable("booking_group");


        // Seed booking Groups
        //var bg1 = Guid.Parse("b5b7a148-0452-4e0e-8298-7c37fd4caa64");
        //var bg2 = Guid.Parse("4658345f-4454-4128-a2f8-c673e73fa846");
        //var bg3 = Guid.Parse("146023a2-4255-4cfd-893a-d04b0839e616");
        //var bg4 = Guid.Parse("1cd16d2e-a35b-48ef-ab93-debd26d445f0");

        modelBuilder.Entity<BookingGroup>().HasData(new BookingGroup
        {
            Id = 1,
            Name = "Offline"
        });
        modelBuilder.Entity<BookingGroup>().HasData(new BookingGroup
        {
            Id = 2,
            Name = "Online"
        });
        modelBuilder.Entity<BookingGroup>().HasData(new BookingGroup
        {
            Id = 3,
            Name = "Dub - In House"
        });
        modelBuilder.Entity<BookingGroup>().HasData(new BookingGroup
        {
            Id = 4,
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
            Id = -1,
            StartDate = DateTime.UtcNow.AddDays(2),
            EndDate = DateTime.UtcNow.AddDays(23),
            ProductionId = prog1,
            LocationId = location1,
            Status = "provisional",
            Notes = "High-speed internet required for remote editing."
        });
        modelBuilder.Entity<Booking>().HasData(new Booking
        {
            Id = -2,
            StartDate = DateTime.UtcNow.AddMonths(1),
            EndDate = DateTime.UtcNow.AddMonths(1).AddDays(7),
            ProductionId = prog2,
            Status = "provisional",
            Notes = "Need access to soundproof dubbing studio."
        });
        modelBuilder.Entity<Booking>().HasData(new Booking
        {
            Id = -3,
            StartDate = DateTime.UtcNow.AddDays(-15),
            EndDate = DateTime.UtcNow.AddDays(-10),
            ProductionId = prog2,
            LocationId = location2,
            Status = "confirmed",
            Notes = "Final editing phase."
        });
        modelBuilder.Entity<Booking>().HasData(new Booking
        {
            Id = -4,
            StartDate = DateTime.UtcNow.AddMonths(-2),
            EndDate = DateTime.UtcNow.AddDays(-5),
            ProductionId = prog3,
            LocationId = location4,
            Status = "confirmed",
            Notes = "Location scouting."
        });
        modelBuilder.Entity<Booking>().HasData(new Booking
        {
            Id = -5,
            StartDate = DateTime.UtcNow.AddMonths(-3),
            EndDate = DateTime.UtcNow.AddMonths(-2).AddDays(-10),
            ProductionId = prog4,
            LocationId = location3,
            Status = "confirmed",
            Notes = "Principal photography."
        });
        modelBuilder.Entity<Booking>().HasData(new Booking
        {
            Id = -6,
            StartDate = DateTime.UtcNow.AddMonths(2),
            EndDate = DateTime.UtcNow.AddMonths(2).AddDays(5),
            ProductionId = prog3,
            LocationId = location4,
            Status = "confirmed",
            Notes = "Pre-production meetings."
        });
        modelBuilder.Entity<Booking>().HasData(new Booking
        {
            Id = -7,
            StartDate = DateTime.UtcNow.AddDays(-8),
            EndDate = DateTime.UtcNow.AddDays(-5),
            ProductionId = prog1,
            LocationId = location1,
            Status = "pending",
            Notes = "Color correction phase."
        });
        modelBuilder.Entity<Booking>().HasData(new Booking
        {
            Id = -8,
            StartDate = DateTime.UtcNow.AddDays(-20),
            EndDate = DateTime.UtcNow.AddDays(-18),
            ProductionId = prog3,
            LocationId = location3,
            Status = "completed",
            Notes = "Sound mixing completed."
        });
        modelBuilder.Entity<Booking>().HasData(new Booking
        {
            Id = -9,
            StartDate = DateTime.UtcNow.AddDays(-12),
            EndDate = DateTime.UtcNow.AddDays(-11),
            ProductionId = prog4,
            LocationId = location4,
            Status = "confirmed",
            Notes = "Reshoots scheduled."
        });
        modelBuilder.Entity<Booking>().HasData(new Booking
        {
            Id = -10,
            StartDate = DateTime.UtcNow.AddDays(-22),
            EndDate = DateTime.UtcNow.AddDays(-20),
            ProductionId = prog4,
            LocationId = location3,
            Status = "cancelled",
            Notes = "Project on hold."
        });
        modelBuilder.Entity<Booking>().HasData(new Booking
        {
            Id = -11,
            StartDate = DateTime.UtcNow.AddDays(-25),
            EndDate = DateTime.UtcNow.AddDays(-23),
            ProductionId = prog4,
            LocationId = location1,
            Status = "completed",
            Notes = "Initial editing phase."
        });
        modelBuilder.Entity<Booking>().HasData(new Booking
        {
            Id = -12,
            StartDate = DateTime.UtcNow.AddDays(-30),
            EndDate = DateTime.UtcNow.AddDays(-28),
            ProductionId = prog4,
            LocationId = location2,
            Status = "confirmed",
            Notes = "Special effects integration."
        });
        modelBuilder.Entity<Booking>().HasData(new Booking
        {
            Id = -13,
            StartDate = DateTime.UtcNow.AddDays(-15),
            EndDate = DateTime.UtcNow.AddDays(-14),
            ProductionId = prog4,
            LocationId = location3,
            Status = "pending",
            Notes = "Waiting for client feedback."
        });
        modelBuilder.Entity<Booking>().HasData(new Booking
        {
            Id = -14,
            StartDate = DateTime.UtcNow.AddDays(-40),
            EndDate = DateTime.UtcNow.AddDays(-35),
            ProductionId = prog3,
            LocationId = location4,
            Status = "completed",
            Notes = "ADR sessions completed."
        });
        modelBuilder.Entity<Booking>().HasData(new Booking
        {
            Id = -15,
            StartDate = DateTime.UtcNow.AddDays(-50),
            EndDate = DateTime.UtcNow.AddDays(-45),
            ProductionId = prog3,
            LocationId = location2,
            Status = "confirmed",
            Notes = "Visual effects review."
        });
        modelBuilder.Entity<Booking>().HasData(new Booking
        {
            Id = -16,
            StartDate = DateTime.UtcNow.AddDays(-5),
            EndDate = DateTime.UtcNow.AddDays(-2),
            ProductionId = prog2,
            LocationId = location1,
            Status = "pending",
            Notes = "Title sequence design."
        });
        modelBuilder.Entity<Booking>().HasData(new Booking
        {
            Id = -17,
            StartDate = DateTime.UtcNow.AddDays(-60),
            EndDate = DateTime.UtcNow.AddDays(-55),
            ProductionId = prog2,
            LocationId = location2,
            Status = "completed",
            Notes = "Final quality check."
        });
        modelBuilder.Entity<Booking>().HasData(new Booking
        {
            Id = -18,
            StartDate = DateTime.UtcNow.AddDays(-18),
            EndDate = DateTime.UtcNow.AddDays(-16),
            ProductionId = prog1,
            LocationId = location3,
            Status = "cancelled",
            Notes = "Client requested changes."
        });
        modelBuilder.Entity<Booking>().HasData(new Booking
        {
            Id = -19,
            StartDate = DateTime.UtcNow.AddDays(-35),
            EndDate = DateTime.UtcNow.AddDays(-32),
            ProductionId = prog2,
            LocationId = location4,
            Status = "confirmed",
            Notes = "New scene additions."
        });
        modelBuilder.Entity<Booking>().HasData(new Booking
        {
            Id = -20,
            StartDate = DateTime.UtcNow.AddDays(-45),
            EndDate = DateTime.UtcNow.AddDays(-40),
            ProductionId = prog3,
            LocationId = location1,
            Status = "completed",
            Notes = "Complete sound design."
        });


        // Seed assets
        modelBuilder.Entity<Equipment>().HasData(new Equipment
        {
            Id = -1,
            Name = "Sony FX6",
            Value = 1000,
            AssetNumber = "12442",
            Make = "Sony",
            Model = "FX-6",
            IsLostOrBroken = false,
            Description = "Faulty lense."
        });
        modelBuilder.Entity<Equipment>().HasData(new Equipment
        {
            Id = -2,
            Name = "Edit01",
            Value = 1000,
            AssetNumber = "12452",
            Make = "Hewlett-Packard",
            Model = "Z4 G4",
            IsLostOrBroken = false,
            Description = "Offline machine"
        });
        modelBuilder.Entity<Equipment>().HasData(new Equipment
        {
            Id = -3,
            Name = "Edit02",
            Value = 1000,
            AssetNumber = "12432",
            Make = "Hewlett-Packard",
            Model = "Z4 G4",
            IsLostOrBroken = false,
            Description = "Offline machine"
        });
        modelBuilder.Entity<Equipment>().HasData(new Equipment
        {
            Id = -4,
            Name = "Edit03",
            Value = 1000,
            AssetNumber = "13352",
            Make = "Hewlett-Packard",
            Model = "Z4 G4",
            IsLostOrBroken = false,
            Description = "Offline machine"
        });
        modelBuilder.Entity<Equipment>().HasData(new Equipment
        {
            Id = -5,
            Name = "Edit04",
            Value = 2000,
            AssetNumber = "23452",
            Make = "Hewlett-Packard",
            Model = "Z8 G4",
            IsLostOrBroken = false,
            Description = "Online machine"
        });
        modelBuilder.Entity<Equipment>().HasData(new Equipment
        {
            Id = -6,
            Name = "Canon C300",
            Value = 1500,
            AssetNumber = "12453",
            Make = "Canon",
            Model = "C300 Mark III",
            IsLostOrBroken = false,
            Description = "New sensor installed."
        });

        modelBuilder.Entity<Equipment>().HasData(new Equipment
        {
            Id = -7,
            Name = "Lighting Kit A",
            Value = 800,
            AssetNumber = "12454",
            Make = "Arri",
            Model = "SKYPANEL S60-C",
            IsLostOrBroken = false,
            Description = "Color tunable LED soft light."
        });

        modelBuilder.Entity<Equipment>().HasData(new Equipment
        {
            Id = -8,
            Name = "Sound Recorder 1",
            Value = 500,
            AssetNumber = "12455",
            Make = "Zoom",
            Model = "H6",
            IsLostOrBroken = false,
            Description = "Portable audio recorder."
        });

        modelBuilder.Entity<Equipment>().HasData(new Equipment
        {
            Id = -9,
            Name = "Drone 1",
            Value = 2000,
            AssetNumber = "12456",
            Make = "DJI",
            Model = "Phantom 4 Pro",
            IsLostOrBroken = false,
            Description = "Aerial shots drone."
        });

        modelBuilder.Entity<Equipment>().HasData(new Equipment
        {
            Id = -10,
            Name = "Edit05",
            Value = 1500,
            AssetNumber = "12457",
            Make = "Apple",
            Model = "iMac Pro",
            IsLostOrBroken = false,
            Description = "Editing workstation."
        });

        modelBuilder.Entity<Equipment>().HasData(new Equipment
        {
            Id = -11,
            Name = "Microphone 1",
            Value = 300,
            AssetNumber = "12458",
            Make = "Sennheiser",
            Model = "MKH 416",
            IsLostOrBroken = false,
            Description = "Shotgun microphone."
        });

        modelBuilder.Entity<Equipment>().HasData(new Equipment
        {
            Id = -12,
            Name = "Gimbal Stabilizer",
            Value = 700,
            AssetNumber = "12459",
            Make = "Zhiyun",
            Model = "Crane 3S",
            IsLostOrBroken = false,
            Description = "Camera stabilizer."
        });

        modelBuilder.Entity<Equipment>().HasData(new Equipment
        {
            Id = -13,
            Name = "Lighting Kit B",
            Value = 1000,
            AssetNumber = "12460",
            Make = "Godox",
            Model = "SL200W",
            IsLostOrBroken = false,
            Description = "Continuous LED light."
        });

        modelBuilder.Entity<Equipment>().HasData(new Equipment
        {
            Id = -14,
            Name = "Edit06",
            Value = 1000,
            AssetNumber = "12461",
            Make = "Hewlett-Packard",
            Model = "Z4 G4",
            IsLostOrBroken = false,
            Description = "Offline machine."
        });

        modelBuilder.Entity<Equipment>().HasData(new Equipment
        {
            Id = -15,
            Name = "Sony A7S III",
            Value = 1800,
            AssetNumber = "12462",
            Make = "Sony",
            Model = "A7S III",
            IsLostOrBroken = false,
            Description = "High sensitivity camera."
        });

        modelBuilder.Entity<Equipment>().HasData(new Equipment
        {
            Id = -16,
            Name = "Field Monitor",
            Value = 400,
            AssetNumber = "12463",
            Make = "Atomos",
            Model = "Ninja V",
            IsLostOrBroken = false,
            Description = "5-inch on-camera monitor."
        });

        modelBuilder.Entity<Equipment>().HasData(new Equipment
        {
            Id = -17,
            Name = "Tripod 1",
            Value = 250,
            AssetNumber = "12464",
            Make = "Manfrotto",
            Model = "MVH500AH",
            IsLostOrBroken = false,
            Description = "Fluid head tripod."
        });

        modelBuilder.Entity<Equipment>().HasData(new Equipment
        {
            Id = -18,
            Name = "Slider 1",
            Value = 500,
            AssetNumber = "12465",
            Make = "Rhino",
            Model = "RŌV Pro",
            IsLostOrBroken = false,
            Description = "Camera slider for smooth shots."
        });

        modelBuilder.Entity<Equipment>().HasData(new Equipment
        {
            Id = -19,
            Name = "Edit07",
            Value = 2000,
            AssetNumber = "12466",
            Make = "Hewlett-Packard",
            Model = "Z8 G4",
            IsLostOrBroken = false,
            Description = "Online machine."
        });

        modelBuilder.Entity<Equipment>().HasData(new Equipment
        {
            Id = -20,
            Name = "Backup Drive 1",
            Value = 300,
            AssetNumber = "12467",
            Make = "Seagate",
            Model = "Expansion 10TB",
            IsLostOrBroken = false,
            Description = "External backup drive."
        });

        // People
        modelBuilder.Entity<Person>().HasData(new Person
        {
            Id = -21,
            Type = "person",
            Name = "Jeremy Cutnice",
            Role = "Offline editor",
            Address = "23 Maple Street, Sheffield, S1 2AB",
            PhoneNumber = "07700 123456",
            Email = "jeremy.cutnice@tvfunmail.com"
        });
        modelBuilder.Entity<Person>().HasData(new Person
        {
            Id = -22,
            Type = "person",
            Name = "Melanie Editswel",
            Role = "Offline editor",
            Address = "45 Oak Avenue, Manchester, M2 3CD",
            PhoneNumber = "07701 234567",
            Email = "melanie.editswel@postprolol.com"
        });
        modelBuilder.Entity<Person>().HasData(new Person
        {
            Id = -23,
            Type = "person",
            Name = "Daphne Showmaker",
            Role = "Offline editor",
            Address = "12 Willow Crescent, Birmingham, B3 4EF",
            PhoneNumber = "07702 345678",
            Email = "daphne.showmaker@tvantics.org"
        });
        modelBuilder.Entity<Person>().HasData(new Person
        {
            Id = -24,
            Type = "person",
            Name = "Percival Televisington-Smythe",
            Role = "Offline editor",
            Address = "8 Birch Lane, Newcastle, NE4 5FG",
            PhoneNumber = "07703 456789",
            Email = "percival.ts@televisingtonsmythe.net"
        });
        modelBuilder.Entity<Person>().HasData(new Person
        {
            Id = -25,
            Type = "person",
            Name = "Dave Programly",
            Role = "Offline editor",
            Address = "36 Elm Close, Liverpool, L5 6GH",
            PhoneNumber = "07704 567890",
            Email = "dave.programly@tvgeekery.co.uk"
        });
        modelBuilder.Entity<Person>().HasData(new Person
        {
            Id = -26,
            Type = "person",
            Name = "Alice Scriptwriter",
            Role = "Scriptwriter",
            Address = "54 Ash Road, Leeds, LS6 7JK",
            PhoneNumber = "07705 678901",
            Email = "alice.scriptwriter@writescripts.com"
        });

        modelBuilder.Entity<Person>().HasData(new Person
        {
            Id = -27,
            Type = "person",
            Name = "Bob Directson",
            Role = "Director",
            Address = "89 Pine Street, Edinburgh, EH8 9AB",
            PhoneNumber = "07706 789012",
            Email = "bob.directson@moviemagic.co.uk"
        });

        modelBuilder.Entity<Person>().HasData(new Person
        {
            Id = -28,
            Type = "person",
            Name = "Claire Soundmix",
            Role = "Sound Mixer",
            Address = "14 Cedar Avenue, Glasgow, G12 8XY",
            PhoneNumber = "07707 890123",
            Email = "claire.soundmix@soundgenius.org"
        });

        modelBuilder.Entity<Person>().HasData(new Person
        {
            Id = -29,
            Type = "person",
            Name = "Dennis Filmor",
            Role = "Cinematographer",
            Address = "21 Spruce Court, Bristol, BS1 3YZ",
            PhoneNumber = "07708 901234",
            Email = "dennis.filmor@cinemagic.com"
        });

        modelBuilder.Entity<Person>().HasData(new Person
        {
            Id = -30,
            Type = "person",
            Name = "Eve Cuttingly",
            Role = "Film Editor",
            Address = "67 Willow Drive, Oxford, OX4 5EF",
            PhoneNumber = "07709 012345",
            Email = "eve.cuttingly@filmeditors.com"
        });

        modelBuilder.Entity<Person>().HasData(new Person
        {
            Id = -31,
            Type = "person",
            Name = "Frank Musicman",
            Role = "Music Composer",
            Address = "33 Fir Road, Cardiff, CF10 1AB",
            PhoneNumber = "07710 123456",
            Email = "frank.musicman@musicscores.co.uk"
        });

        modelBuilder.Entity<Person>().HasData(new Person
        {
            Id = -32,
            Type = "person",
            Name = "Grace Storyteller",
            Role = "Screenwriter",
            Address = "25 Alder Lane, Nottingham, NG3 2GH",
            PhoneNumber = "07711 234567",
            Email = "grace.storyteller@writingscripts.com"
        });

        modelBuilder.Entity<Person>().HasData(new Person
        {
            Id = -33,
            Type = "person",
            Name = "Hank Foley",
            Role = "Foley Artist",
            Address = "18 Chestnut Boulevard, Cambridge, CB1 2IJ",
            PhoneNumber = "07712 345678",
            Email = "hank.foley@foleyartistry.com"
        });

        modelBuilder.Entity<Person>().HasData(new Person
        {
            Id = -34,
            Type = "person",
            Name = "Isla Graphics",
            Role = "Graphic Designer",
            Address = "9 Redwood Close, Brighton, BN1 3KL",
            PhoneNumber = "07713 456789",
            Email = "isla.graphics@designcreations.co.uk"
        });

        modelBuilder.Entity<Person>().HasData(new Person
        {
            Id = -35,
            Type = "person",
            Name = "Jack Animator",
            Role = "Animator",
            Address = "40 Beech Hill, Reading, RG1 4MN",
            PhoneNumber = "07714 567890",
            Email = "jack.animator@animationhub.com"
        });

        modelBuilder.Entity<Person>().HasData(new Person
        {
            Id = -36,
            Type = "person",
            Name = "Karen Lights",
            Role = "Lighting Technician",
            Address = "73 Sycamore Lane, Belfast, BT7 8OP",
            PhoneNumber = "07715 678901",
            Email = "karen.lights@lightsup.com"
        });

        modelBuilder.Entity<Person>().HasData(new Person
        {
            Id = -37,
            Type = "person",
            Name = "Leo Cameraman",
            Role = "Camera Operator",
            Address = "61 Maple Terrace, Aberdeen, AB10 1QR",
            PhoneNumber = "07716 789012",
            Email = "leo.cameraman@cameraaction.com"
        });

        modelBuilder.Entity<Person>().HasData(new Person
        {
            Id = -38,
            Type = "person",
            Name = "Mia Soundcheck",
            Role = "Sound Engineer",
            Address = "48 Elm Street, York, YO10 3ST",
            PhoneNumber = "07717 890123",
            Email = "mia.soundcheck@soundstudio.org"
        });

        modelBuilder.Entity<Person>().HasData(new Person
        {
            Id = -39,
            Type = "person",
            Name = "Nate Propman",
            Role = "Props Master",
            Address = "11 Cedar Grove, Leicester, LE2 7UV",
            PhoneNumber = "07718 901234",
            Email = "nate.propman@propscreative.com"
        });

        modelBuilder.Entity<Person>().HasData(new Person
        {
            Id = -40,
            Type = "person",
            Name = "Olivia Costume",
            Role = "Costume Designer",
            Address = "19 Poplar Close, Norwich, NR1 4WX",
            PhoneNumber = "07719 012345",
            Email = "olivia.costume@costumecrafts.co.uk"
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
                    entry.Entity.CreatedDate = DateTime.UtcNow;
                    entry.Entity.CreatedBy = _loggedInUserService.UserId;
                    break;
                case EntityState.Modified:
                    entry.Entity.LastModifiedDate = DateTime.UtcNow;
                    entry.Entity.LastModifiedBy = _loggedInUserService.UserId;
                    break;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}
