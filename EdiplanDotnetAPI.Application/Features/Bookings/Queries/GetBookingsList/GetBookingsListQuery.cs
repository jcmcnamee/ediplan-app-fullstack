using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace EdiplanDotnetAPI.Application.Features.Bookings.Queries.GetBookingsList;

public class GetBookingsListQuery : IRequest<List<BookingListVm>>
{

}
