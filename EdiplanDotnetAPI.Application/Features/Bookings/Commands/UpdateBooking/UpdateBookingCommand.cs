using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdiplanDotnetAPI.Application.Features.Bookings.Commands.UpdateBooking;
public class UpdateBookingCommand : IRequest
{
    public int Id { get; set; }
    public string Name { get; set; } = "New booking.";
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsConfirmed { get; set; } = true;
    public string? Notes { get; set; }
    public Guid? ProductionId { get; set; }
    public Guid? LocationId { get; set; }
}
