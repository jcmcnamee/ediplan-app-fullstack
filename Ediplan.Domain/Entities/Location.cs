using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ediplan.Domain.Entities;

public class Location
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "New location";
    public string Notes { get; set; } = "";

    public List<Booking> Bookings { get; } = new();
}
