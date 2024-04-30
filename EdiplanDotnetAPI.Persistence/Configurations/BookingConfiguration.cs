using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EdiplanDotnetAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EdiplanDotnetAPI.Persistence.Configurations;

public class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {   
        builder.Property(b => b.Name).IsRequired().HasMaxLength(50);
    }
}
