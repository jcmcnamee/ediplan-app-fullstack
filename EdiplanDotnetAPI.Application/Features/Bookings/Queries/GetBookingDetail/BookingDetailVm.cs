using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdiplanDotnetAPI.Application.Features.Bookings.Queries.GetBookingDetail;

public class BookingDetailVm
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsConfirmed { get; set; }
    public string Notes { get; set; } = string.Empty;
    public ProductionDto? Production { get; set; }
    public LocationDto? Location { get; set; }
}
