using EdiplanDotnetAPI.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdiplanDotnetAPI.Application.Features.Bookings.Commands.CreateBooking;
public class CreateBookingCommandResponse : BaseResponse
{
    public CreateBookingCommandResponse() : base()
    {

    }

    public CreateBookingDto Booking { get; set; } = default!;
}
