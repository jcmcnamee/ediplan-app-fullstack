using EdiplanDotnetAPI.Application.Features.BookingGroups.Queries.GetBookingGroupDetail;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdiplanDotnetAPI.Application.Features.BookingGroups.Queries.GetBookingGroupMembers;
internal class GetBookingGroupMemberQuery : IRequest<List<BookingGroupMemberListVm>>
{
    public bool IncludeHistory { get; set; }
}
