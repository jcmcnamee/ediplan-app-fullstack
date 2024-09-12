using Ediplan.Domain.Entities;
using Ediplan.Persistence.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Ediplan.Persistence.Configurations;

public class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.Property(b => b.Name).IsRequired().HasMaxLength(50);

        builder.Property(b => b.StartDate)
            .HasConversion(
                v => v.ToUniversalTime(),
                v => TruncateToSeconds.Truncate(DateTime.SpecifyKind(v, DateTimeKind.Utc)));

        builder.Property(b => b.EndDate)
            .HasConversion(
                v => v.ToUniversalTime(),
                v => TruncateToSeconds.Truncate(DateTime.SpecifyKind(v, DateTimeKind.Utc)));

        builder
            .HasMany(b => b.BookingGroups)
            .WithMany(b => b.Bookings)
            .UsingEntity("booking_group_map");

        builder.ToTable("booking");
    }
}
