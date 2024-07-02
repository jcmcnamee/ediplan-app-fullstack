using MediatR;

namespace Ediplan.Application.Features.BookingGroups.Queries.GetBookingGroupsList;

public class GetBookingGroupListQuery : IRequest<List<BookingGroupListVm>>
{
}

