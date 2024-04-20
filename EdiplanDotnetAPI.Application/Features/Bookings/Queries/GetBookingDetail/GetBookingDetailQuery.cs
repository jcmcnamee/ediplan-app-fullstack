using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace EdiplanDotnetAPI.Application.Features.Bookings.Queries.GetBookingDetail;

public class GetBookingDetailQuery : IRequest<BookingDetailVm>
{
    public Guid Id { get; set; }
}
