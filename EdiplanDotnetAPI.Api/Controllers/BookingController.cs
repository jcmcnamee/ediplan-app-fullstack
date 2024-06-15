using EdiplanDotnetAPI.Application.Features.Bookings.Queries.GetBookingDetail;
using EdiplanDotnetAPI.Application.Features.Bookings.Queries.GetBookingsList;
using EdiplanDotnetAPI.Application.Features.Bookings.Commands.UpdateBooking;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using EdiplanDotnetAPI.Application.Features.Bookings.Commands.DeleteBooking;
using EdiplanDotnetAPI.Application.Features.Bookings.Queries.GetBookingsExport;
using EdiplanDotnetAPI.Api.Utility;
using EdiplanDotnetAPI.Application.Features.Bookings.Commands.CreateBooking;
using EdiplanDotnetAPI.Application.Features.Bookings.Commands.PatchBooking;
using Microsoft.AspNetCore.JsonPatch;
using EdiplanDotnetAPI.Domain.Entities;

namespace EdiplanDotnetAPI.Api.Controllers;

[ApiController]
[Route("api/bookings")]
public class BookingController : ControllerBase
{
    private readonly IMediator _mediator;

    public BookingController(IMediator mediator)
    {
        _mediator = mediator;

    }

    [HttpGet(Name = "GetBookings")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<List<BookingListVm>>> GetBookings([FromQuery] GetBookingsListQuery bookingResourceParams)
    {
        var result = await _mediator.Send(bookingResourceParams);
        return Ok(result);
    }

    [HttpGet("{id}", Name = "GetBookingById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<BookingDetailVm>> GetBookingById(int id)
    {
        var getBookingDetailQuery = new GetBookingDetailQuery() { Id = id };
        return Ok(await _mediator.Send(getBookingDetailQuery));
    }

    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    [HttpPost(Name = "CreateBooking")]
    public async Task<ActionResult<CreateBookingCommandResponse>> Create([FromBody] CreateBookingCommand createBookingCommand)
    {
        var response = await _mediator.Send(createBookingCommand);
        return CreatedAtRoute("GetBookingById", new { Id = response.Booking.Id }, response);
    }

    [HttpPut("{id}", Name = "UpdateBooking")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Update(int id, [FromBody] UpdateBookingCommand updateBookingCommand)
    {
        updateBookingCommand.Id = id;
        await _mediator.Send(updateBookingCommand);
        return NoContent();
    }

    [HttpPatch("{id}", Name = "PatchBooking")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> PartialUpdate(int id, [FromBody] JsonPatchDocument<UpdateBookingDto> patchDocument)
    {
        PatchBookingCommand patchBookingCommand = new PatchBookingCommand {
            Id = id,
            Booking = patchDocument
        };
        await _mediator.Send(patchBookingCommand);
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

    [HttpGet("export", Name = "ExportBookings")]
    [FileResultContentType("text/csv")]
    [ProducesDefaultResponseType]
    public async Task<FileResult> ExportBookings()
    {
        var fileDto = await _mediator.Send(new GetBookingsExportQuery());

        return File(fileDto.Data, fileDto.ContentType, fileDto.BookingExportFileName);

    }

    // Add HttpOptions support


}
