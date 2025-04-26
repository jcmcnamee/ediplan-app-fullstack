using Ediplan.Api.Utility;
using Ediplan.Application.Features.Bookings.Queries.GetBookingsList;
using Ediplan.Application.Helpers;
using Ediplan.Application.Models;
using System.Text.Json;

namespace Ediplan.Api.Controllers;

public partial class BookingController
{
    // TODO: Currently controller breaks SRP. Refactor calculations to services.

    /// <summary>
    /// Add pagination metadata to the response headers.
    /// </summary>
    /// <param name="bookingResourceParams"></param>
    /// <param name="result"></param>
    private void AddPaginationMetadata(GetBookingsListQuery bookingResourceParams, PagedList<BookingListVm> result)
    {
        var previousPageLink = result.HasPrevious ? CreateBookingsResourceUri(
                    bookingResourceParams, ResourceUriType.PreviousPage) : null;

        var nextPageLink = result.HasNext ? CreateBookingsResourceUri(
            bookingResourceParams, ResourceUriType.NextPage) : null;

        var paginationMetadata = _paginationMetadataService.CreatePaginationMetadata(result, nextPageLink, previousPageLink);

        Response.Headers.Append("X-Pagination", JsonSerializer.Serialize
            (paginationMetadata));
    }

    //private global::System.Object CreatePaginationMetadata(GetBookingsListQuery bookingResourceParams, PagedList<BookingListVm> result)
    //{
    //    var previousPageLink = result.HasPrevious ? CreateBookingsResourceUri(
    //                bookingResourceParams, ResourceUriType.PreviousPage) : null;

    //    var nextPageLink = result.HasNext ? CreateBookingsResourceUri(
    //        bookingResourceParams, ResourceUriType.NextPage) : null;

    //    var paginationMetadata = new
    //    {
    //        totalCount = result.TotalCount,
    //        pageSize = result.PageSize,
    //        currentPage = result.CurrentPage,
    //        totalPages = result.TotalPages,
    //        previousPageLink = previousPageLink,
    //        nextPageLink = nextPageLink
    //    };
    //    return paginationMetadata;
    //}

    /// <summary>
    /// Create a resource URI for bookings.
    /// </summary>
    /// <param name="bookingResourceParams"></param>
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

    // Not sure this is required.......
    public string? CreateBookingsResourceUri(string routeName, GetBookingsListQuery bookingResourceParams, ResourceUriType type)
    {
        switch (type)
        {
            case ResourceUriType.PreviousPage:
                return Url.Link(routeName, new
                {
                    sortBy = bookingResourceParams.SortBy,
                    page = bookingResourceParams.Page - 1,
                    pageSize = bookingResourceParams.PageSize,
                    status = bookingResourceParams.Status,
                    search = bookingResourceParams.Search
                });
            case ResourceUriType.NextPage:
                return Url.Link(routeName, new
                {
                    sortBy = bookingResourceParams.SortBy,
                    page = bookingResourceParams.Page + 1,
                    pageSize = bookingResourceParams.PageSize,
                    status = bookingResourceParams.Status,
                    search = bookingResourceParams.Search
                });
            default:
                return Url.Link(routeName, new
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
    /// Create links for booking resource.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="fields"></param>
    /// <returns></returns>
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

    

}
