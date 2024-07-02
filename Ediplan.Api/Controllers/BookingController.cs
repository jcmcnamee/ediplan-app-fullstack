using Ediplan.Application.Features.Bookings.Queries.GetBookingDetail;
using Ediplan.Application.Features.Bookings.Queries.GetBookingsList;
using Ediplan.Application.Features.Bookings.Commands.UpdateBooking;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ediplan.Application.Features.Bookings.Commands.DeleteBooking;
using Ediplan.Application.Features.Bookings.Queries.GetBookingsExport;
using Ediplan.Api.Utility;
using Ediplan.Application.Features.Bookings.Commands.CreateBooking;
using Ediplan.Application.Features.Bookings.Commands.PatchBooking;
using Microsoft.AspNetCore.JsonPatch;
using Ediplan.Domain.Entities;
using Ediplan.Application.Helpers;
using System.Text.Json;
using Ediplan.Application.Services;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Ediplan.Application.Exceptions;
using Ediplan.Application.Models;
using Ediplan.Api.Services;

namespace Ediplan.Api.Controllers;

[ApiController]
[Route("api/bookings")]
public class BookingController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IPropertyMappingService _propertyMappingService;
    private readonly IPropertyCheckerService _propertyCheckerService;
    private readonly ProblemDetailsFactory _problemDetailsFactory;

    public BookingController(IMediator mediator, IPropertyMappingService propertyMappingService, IPropertyCheckerService propertyCheckerService, ProblemDetailsFactory problemDetailsFactory)
    {
        _mediator = mediator ??
            throw new ArgumentNullException(nameof(mediator));
        _propertyMappingService = propertyMappingService ??
            throw new ArgumentNullException(nameof(propertyMappingService));
        _propertyCheckerService = propertyCheckerService ??
            throw new ArgumentNullException(nameof(propertyCheckerService));
        _problemDetailsFactory = problemDetailsFactory ??
            throw new ArgumentNullException(nameof(problemDetailsFactory));
    }


    /// <summary>
    /// Returns all bookings
    /// </summary>
    /// <param name="bookingResourceParams"></param>
    /// <returns></returns>
    [HttpGet(Name = "GetBookings")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<BookingListVm>> GetBookings([FromQuery] GetBookingsListQuery bookingResourceParams)
    {
        if (!_propertyMappingService.ValidMappingExistsFor<BookingListVm, Booking>(
            bookingResourceParams.SortBy))
        {
            return BadRequest();
        }

        if (!_propertyCheckerService.TypeHasProperties<BookingListVm>(
            bookingResourceParams.Fields))
        {
            return BadRequest(
                _problemDetailsFactory.CreateProblemDetails(
                    HttpContext,
                    statusCode: 400,
                    detail: $"Not all requested data shaping fields exist on " +
                        $"the resource: {bookingResourceParams.Fields}"));
        }

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

        return Ok(result.AsEnumerable().ShapeData(bookingResourceParams.Fields));
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
                    sortBy = bookingResourceParams.SortBy,
                    page = bookingResourceParams.Page - 1,
                    pageSize = bookingResourceParams.PageSize,
                    status = bookingResourceParams.Status,
                    search = bookingResourceParams.Search
                });
            case ResourceUriType.NextPage:
                return Url.Link("GetBookings", new
                {
                    sortBy = bookingResourceParams.SortBy,
                    page = bookingResourceParams.Page + 1,
                    pageSize = bookingResourceParams.PageSize,
                    status = bookingResourceParams.Status,
                    search = bookingResourceParams.Search
                });
            default:
                return Url.Link("GetBookings", new
                {
                    sortBy = bookingResourceParams.SortBy,
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
    public async Task<ActionResult<BookingDetailVm>> GetBookingById(int id, string? fields)
    {
        // Field checks
        if (!_propertyCheckerService.TypeHasProperties<BookingDetailVm>(fields))
        {
            return BadRequest(
                _problemDetailsFactory.CreateProblemDetails(HttpContext,
                statusCode: 400, detail: $"Not all requested data shaping fields exist on " +
                $"the resource: {fields}"));
        }

        // Make query and send
        var getBookingDetailQuery = new GetBookingDetailQuery() { Id = id };
        var booking = await _mediator.Send(getBookingDetailQuery);


        if (booking == null)
        {
            return NotFound();
        }

        return Ok(booking.ShapeData(fields));
    }

    private IEnumerable<LinkDto> CreateLinksForBooking(int id, string? fields)
    {
        var links = new List<LinkDto>();

        if (string.IsNullOrWhiteSpace(fields))
        {
            links.Add(
                new(Url.Link("GetBookingById", new { id }),
                "self",
                "GET"));
        }
        else
        {
            links.Add(
                new(Url.Link("GetBookingById", new { id, fields }),
                "self",
                "GET"));
        }

        return links;
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

        var links = CreateLinksForBooking(response.Booking.Id, null);
        response.Links = links;

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
