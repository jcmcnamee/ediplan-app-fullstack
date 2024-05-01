using EdiplanDotnetAPI.Application.Features.Bookings.Queries.GetBookingDetail;
using EdiplanDotnetAPI.Application.Features.Bookings.Queries.GetBookingsList;
using EdiplanDotnetAPI.Application.Features.Bookings.Commands.UpdateBooking;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using EdiplanDotnetAPI.Application.Features.Bookings.Commands.DeleteBooking;

namespace EdiplanDotnetAPI.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingController : ControllerBase
{
    private readonly IMediator _mediator;

    public BookingController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet(Name = "GetAllBookings")]
    [ProducesResponseType(StatusCodes.Status200OK)] // Documentation
    [ProducesDefaultResponseType]
    public async Task<ActionResult<List<BookingListVm>>> GetAllBookings()
    {
        var result = await _mediator.Send(new GetBookingsListQuery());
        return Ok(result);
    }

    [HttpGet("{id}", Name = "GetBookingById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<BookingDetailVm>> GetBookingById(int id)
    {
        var getBookingDetailQuery = new GetBookingDetailQuery() { Id = id };
        return Ok(await _mediator.Send(getBookingDetailQuery));
    }

    [HttpPost(Name = "UpdateBooking")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Update([FromBody] UpdateBookingCommand updateBookingCommand)
    {
        await _mediator.Send(updateBookingCommand);
        return NoContent();
    }

    [HttpDelete("{id}", Name = "DeleteBooking")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Delete(int id)
    {
        var deleteBookingCommand = new DeleteBookingCommand() { Id = id };
        await _mediator.Send(deleteBookingCommand);
        return NoContent();
    }


}
