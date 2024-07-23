using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Threading.Tasks;
using Ediplan.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ediplan.Persistence.Configurations;

public class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {   
        builder.Property(b => b.Name).IsRequired().HasMaxLength(50);

        builder.Property(b => b.StartDate)
            .HasConversion(
                v => v.ToUniversalTime(),
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

        builder.Property(b => b.EndDate)
            .HasConversion(
            v => v.ToUniversalTime(),
            v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
    }
}
