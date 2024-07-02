using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ediplan.Domain.Entities;

public class BookingGroup
{
    public int Id { get; set; }
    public string Name { get; set; } = "New Group";

    // Navigation properties
    public List<Booking> Bookings { get; } = new();
}
