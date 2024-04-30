using EdiplanDotnetAPI.Application.Features.BookingGroups.Queries.GetBookingGroupsList;
using EdiplanDotnetAPI.Application.Features.BookingGroups.Queries.GetBookingGroupMembers;
using EdiplanDotnetAPI.Application.Features.BookingGroups.Commands.CreateBookingGroup;
using EdiplanDotnetAPI.Application.Features.Bookings.Queries.GetBookingsList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EdiplanDotnetAPI.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookingGroupController : ControllerBase
{
    private readonly IMediator _mediator;

    public BookingGroupController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("all", Name = "GetAllBookingGroups")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<BookingGroupListVm>>> GetAllBookingGroups()
    {
        var dtos = await _mediator.Send(new GetBookingGroupListQuery());
        return Ok(dtos);
    }

    [HttpGet("allwithbookings", Name = "GetGroupsWithBookings")]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<BookingGroupMemberListVm>>> GetBookingGroupsWithBookings(bool includeHistory)
    {
        GetBookingGroupMemberQuery getGroupsListWithMembersQuery = new GetBookingGroupMemberQuery()
        {
            IncludeHistory = includeHistory
        };

        var dtos = await _mediator.Send(getGroupsListWithMembersQuery);
        return Ok(dtos);
    }

    [HttpPost(Name = "AddBookingGroup")]
    public async Task<ActionResult<CreateBookingGroupCommandResponse>> Create([FromBody] CreateBookingGroupCommand createGroupCommand)
    {
        var response = await _mediator.Send(createGroupCommand);
        return Ok(response);
    }

}
