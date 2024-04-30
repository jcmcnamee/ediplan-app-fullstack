using MediatR;

namespace EdiplanDotnetAPI.Application.Features.BookingGroups.Queries.GetBookingGroupMembers;
public class GetBookingGroupMemberQuery : IRequest<List<BookingGroupMemberListVm>>
{
    public bool IncludeHistory { get; set; }
}
