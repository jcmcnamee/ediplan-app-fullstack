using Ediplan.Api.Utility;
using Ediplan.Application.Features.Bookings.Commands.CreateBooking;
using Ediplan.Application.Features.Bookings.Commands.DeleteBooking;
using Ediplan.Application.Features.Bookings.Commands.PatchBooking;
using Ediplan.Application.Features.Bookings.Commands.UpdateBooking;
using Ediplan.Application.Features.Bookings.Queries.GetBookingDetail;
using Ediplan.Application.Features.Bookings.Queries.GetBookingsExport;
using Ediplan.Application.Features.Bookings.Queries.GetBookingsList;
using Ediplan.Application.Helpers;
using Ediplan.Application.Services;
using Ediplan.Domain.Entities;
using Marvin.Cache.Headers;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Ediplan.Api.Controllers;

[ApiController]
[Route("api/bookings")]
public partial class BookingController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IPropertyMappingService _propertyMappingService;
    private readonly IPropertyCheckerService _propertyCheckerService;
    private readonly ProblemDetailsFactory _problemDetailsFactory;
    private readonly IPaginationMetadataService _paginationMetadataService;

    public BookingController(IMediator mediator, IPropertyMappingService propertyMappingService, IPropertyCheckerService propertyCheckerService, ProblemDetailsFactory problemDetailsFactory, IPaginationMetadataService paginationMetadataService)
    {
        _mediator = mediator ??
            throw new ArgumentNullException(nameof(mediator));
        _propertyMappingService = propertyMappingService ??
            throw new ArgumentNullException(nameof(propertyMappingService));
        _propertyCheckerService = propertyCheckerService ??
            throw new ArgumentNullException(nameof(propertyCheckerService));
        _problemDetailsFactory = problemDetailsFactory ??
            throw new ArgumentNullException(nameof(problemDetailsFactory));
        _paginationMetadataService = paginationMetadataService ??
            throw new ArgumentNullException(nameof(paginationMetadataService));
    }


    /// <summary>
    /// Get a list of bookings with optional filtering, sorting, shaping and pagination.
    /// </summary>
    /// <param name="bookingResourceParams"></param>
    /// <returns></returns>
    [HttpGet(Name = "GetBookings")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<BookingListVm>> GetBookings([FromQuery] GetBookingsListQuery bookingResourceParams)
    {
        // Validate any property mappings
        if (!_propertyMappingService.ValidMappingExistsFor<BookingListVm, Booking>(
            bookingResourceParams.SortBy))
        {
            return BadRequest(_problemDetailsFactory
                .CreateProblemDetails(
                    HttpContext,
                    statusCode: 400,
                    detail: $"Not all requested mapping fields exist on " +
                        $"the resource: {bookingResourceParams.SortBy}"
                        )
                );
        }

        // Validate fields for data shaping
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

        // Get the results
        var result = await _mediator.Send(bookingResourceParams);
        if (result == null)
        {
            return NoContent();
        }

        // Apply pagination and shaping
        AddPaginationMetadata(bookingResourceParams, result);

        // TODO: Add HATEOS links? Will have to return a response object instead...

        return Ok(result.AsEnumerable().ShapeData(bookingResourceParams.Fields));
    }

    /// <summary>
    /// Get a booking by ID with optional data shaping.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="fields"></param>
    /// <returns></returns>
    [HttpGet("{id}", Name = "GetBookingById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    //[ResponseCache(CacheProfileName = "120SecondsCacheProfile")]
    [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 60)]
    [HttpCacheValidation(MustRevalidate = true)]
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

    /// <summary>
    /// Create a new booking.
    /// </summary>
    /// <param name="createBookingCommand"></param>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesDefaultResponseType]
    [HttpPost(Name = "CreateBooking")]
    public async Task<ActionResult<CreateBookingCommandResponse>> Create([FromBody] CreateBookingCommand createBookingCommand)
    {
        var response = await _mediator.Send(createBookingCommand);

        // TODO: Add conditional logic for failed insert:
        var links = CreateLinksForBooking(response.Booking.Id, null);
        response.Links = links;

        return CreatedAtRoute("GetBookingById", new { Id = response.Booking.Id }, response);
    }

    /// <summary>
    /// Update a booking by ID.
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
    /// Partially update a booking by ID using JSON Patch.
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
    /// Delete a booking by ID.
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
    /// Export bookings to a CSV file.
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

}
