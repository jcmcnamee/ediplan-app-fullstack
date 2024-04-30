using AutoMapper;
using EdiplanDotnetAPI.Application.Contracts.Persistence;
using MediatR;

namespace EdiplanDotnetAPI.Application.Features.BookingGroups.Queries.GetBookingGroupMembers;
public class GetBookingGroupMemberQueryHandler : IRequestHandler<GetBookingGroupMemberQuery, List<BookingGroupMemberListVm>>
{
    private readonly IMapper _mapper;
    private readonly IBookingGroupRepository _bookingGroupRepository;

    public GetBookingGroupMemberQueryHandler(IMapper mapper, IBookingGroupRepository bookingGroupRepository)
    {
        _mapper = mapper;
        _bookingGroupRepository = bookingGroupRepository;
    }
    public async Task<List<BookingGroupMemberListVm>> Handle(GetBookingGroupMemberQuery request, CancellationToken cancellationToken)
    {
        var list = await _bookingGroupRepository.GetBookingGroupsWithMembers(request.IncludeHistory);
        return _mapper.Map<List<BookingGroupMemberListVm>>(list);
    }
}
