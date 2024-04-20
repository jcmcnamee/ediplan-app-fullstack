using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdiplanDotnetAPI.Domain.Entities;

public class Location
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "New location";
    public string Notes { get; set; } = "";

    public ICollection<Booking> Bookings { get; } = new List<Booking>();

    public Location(string name)
    {
        Name = name;
    }
}
