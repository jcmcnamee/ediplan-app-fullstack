using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdiplanDotnetAPI.Domain.Entities;

public class BookingGroup
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "New Group";

    // Navigation properties
    public ICollection<Booking>? Bookings { get; set; }
}
