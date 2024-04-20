using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdiplanDotnetAPI.Application.Features.Bookings.Commands.DeleteBooking;
internal class DeleteBookingCommand : IRequest
{
    public Guid Id { get; set; }
}
