using Ediplan.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ediplan.Application.Features.Bookings.Commands.CreateBooking;
public class CreateBookingCommand : IRequest<CreateBookingCommandResponse>
{
    public string Name { get; set; } = "New booking.";
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? Notes { get; set; }
    public Guid? ProductionId { get; set; }
    public Guid? LocationId { get; set; }
    public List<int>? AssetIds { get; set; }
    public List<int>? BookingGroupIds { get; set; }

    public override string ToString()
    {
        var bookingGroupIdsString = BookingGroupIds != null ? string.Join(", ", BookingGroupIds) : "None";
        return $"Booking name: {Name}; Start Date: {StartDate}; End Date: {EndDate}; Is Confirmed: {Status}; Notes: {Notes}; Production ID: {ProductionId}; Location ID: {LocationId}; Asset Ids: {AssetIds}; BookingGroup Ids: {bookingGroupIdsString}";
    }
}
