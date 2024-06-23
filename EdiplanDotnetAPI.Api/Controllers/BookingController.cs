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
using EdiplanDotnetAPI.Application.Helpers;
using System.Text.Json;

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


    /// <summary>
    /// 
    /// </summary>
    /// <param name="bookingResourceParams"></param>
    /// <returns></returns>
    [HttpGet(Name = "GetBookings")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<BookingListVm>> GetBookings([FromQuery] GetBookingsListQuery bookingResourceParams)
    {
        var result = await _mediator.Send(bookingResourceParams);
        if (result == null)
        {
            return NoContent();
        }

        var previousPageLink = result.HasPrevious ? CreateBookingsResourceUri(
            bookingResourceParams, ResourceUriType.PreviousPage) : null;

        var nextPageLink = result.HasNext ? CreateBookingsResourceUri(
            bookingResourceParams, ResourceUriType.NextPage) : null;

        var paginationMetadata = new
        {
            totalCount = result.TotalCount,
            pageSize = result.PageSize,
            currentPage = result.CurrentPage,
            totalPages = result.TotalPages,
            previousPageLink = previousPageLink,
            nextPageLink = nextPageLink
        };

        Response.Headers.Append("X-Pagination", JsonSerializer.Serialize
            (paginationMetadata));

        return Ok(result.ShapeData(bookingResourceParams.Fields));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="bookingsListQuery"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    private string? CreateBookingsResourceUri(GetBookingsListQuery bookingResourceParams, ResourceUriType type)
    {
        switch (type)
        {
            case ResourceUriType.PreviousPage:
                return Url.Link("GetBookings", new
                {
                    orderBy = bookingResourceParams.OrderBy,
                    page = bookingResourceParams.Page - 1,
                    pageSize = bookingResourceParams.PageSize,
                    status = bookingResourceParams.Status,
                    search = bookingResourceParams.Search
                });
            case ResourceUriType.NextPage:
                return Url.Link("GetBookings", new
                {
                    orderBy = bookingResourceParams.OrderBy,
                    page = bookingResourceParams.Page + 1,
                    pageSize = bookingResourceParams.PageSize,
                    status = bookingResourceParams.Status,
                    search = bookingResourceParams.Search
                });
            default:
                return Url.Link("GetBookings", new
                {
                    orderBy = bookingResourceParams.OrderBy,
                    page = bookingResourceParams.Page,
                    pageSize = bookingResourceParams.PageSize,
                    status = bookingResourceParams.Status,
                    search = bookingResourceParams.Search
                });
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}", Name = "GetBookingById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<BookingDetailVm>> GetBookingById(int id)
    {
        var getBookingDetailQuery = new GetBookingDetailQuery() { Id = id };
        return Ok(await _mediator.Send(getBookingDetailQuery));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="createBookingCommand"></param>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    [HttpPost(Name = "CreateBooking")]
    public async Task<ActionResult<CreateBookingCommandResponse>> Create([FromBody] CreateBookingCommand createBookingCommand)
    {
        var response = await _mediator.Send(createBookingCommand);
        return CreatedAtRoute("GetBookingById", new { Id = response.Booking.Id }, response);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="updateBookingCommand"></param>
    /// <returns></returns>
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="patchDocument"></param>
    /// <returns></returns>
    [HttpPatch("{id}", Name = "PatchBooking")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> PartialUpdate(int id, [FromBody] JsonPatchDocument<UpdateBookingDto> patchDocument)
    {
        PatchBookingCommand patchBookingCommand = new PatchBookingCommand
        {
            Id = id,
            Booking = patchDocument
        };
        await _mediator.Send(patchBookingCommand);
        return NoContent();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
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

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
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
