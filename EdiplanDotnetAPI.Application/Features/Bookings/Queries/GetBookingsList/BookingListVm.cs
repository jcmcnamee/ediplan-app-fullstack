using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EdiplanDotnetAPI.Application.Features.Bookings.Queries.GetBookingsList;

public class BookingListVm
{
    public Guid BookingId { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
