using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdiplanDotnetAPI.Application.Features.BookingGroups.Queries.GetBookingGroupsList;

internal class GetBookingGroupListQuery : IRequest<List<BookingGroupListVm>>
{
}

