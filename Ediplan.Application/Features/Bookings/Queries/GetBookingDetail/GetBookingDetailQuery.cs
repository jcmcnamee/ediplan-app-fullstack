using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace Ediplan.Application.Features.Bookings.Queries.GetBookingDetail;

public class GetBookingDetailQuery : IRequest<BookingDetailVm>
{
    public int Id { get; set; }
}
