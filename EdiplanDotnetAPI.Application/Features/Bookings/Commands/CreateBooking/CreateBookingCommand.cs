using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdiplanDotnetAPI.Application.Features.Bookings.Commands.CreateBooking;
public class CreateBookingCommand : IRequest<CreateBookingCommandResponse>
{
    public string Name { get; set; } = "New booking.";
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? Notes { get; set; }
    public Guid? ProductionId { get; set; }
    public Guid? LocationId { get; set; }
    public override string ToString()
    {
        return $"Booking name: {Name}; Start Date: {StartDate}; End Date: {EndDate}; Is Confirmed: {Status}; Notes: {Notes}; Production ID: {ProductionId}; Location ID: {LocationId}";
    }
}
