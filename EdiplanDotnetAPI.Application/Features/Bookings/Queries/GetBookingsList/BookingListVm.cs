using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EdiplanDotnetAPI.Application.Features.Bookings.Queries.GetBookingsList;

public class BookingListVm
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ProductionName { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string LocationName { get; set; } = string.Empty;
    public string Status { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime LastModifiedDate { get; set;}
}
