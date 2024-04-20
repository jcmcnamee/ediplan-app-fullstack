using AutoMapper;
using EdiplanDotnetAPI.Application.Contracts.Persistence;
using EdiplanDotnetAPI.Application.Features.BookingGroups.Queries.GetBookingGroupDetail;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdiplanDotnetAPI.Application.Features.BookingGroups.Queries.GetBookingGroupMembers;
internal class GetBookingGroupMemberQueryHandler : IRequestHandler<GetBookingGroupMemberQuery, List<BookingGroupMemberListVm>>
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
