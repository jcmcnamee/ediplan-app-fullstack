using Ediplan.Application.Features.BookingGroups.Queries.GetBookingGroupsList;
using Ediplan.Application.Features.Bookings.Queries.GetBookingsList;
using Ediplan.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Ediplan.Api.Utility;

public class BookingUriService : IBookingUriService
{
    private readonly IUrlHelper _urlHelper;

    public BookingUriService(IUrlHelper urlHelper)
    {
        _urlHelper = urlHelper;
    }

    public string? CreateBookingsResourceUri(string routeName, GetBookingGroupListQuery bookingResourceParams, ResourceUriType type)
    {
        throw new NotImplementedException();
        //switch (type)
        //{
        //    case ResourceUriType.PreviousPage:
        //        return Url.Link("GetBookings", new
        //        {
        //            sortBy = bookingResourceParams.SortBy,
        //            page = bookingResourceParams.Page - 1,
        //            pageSize = bookingResourceParams.PageSize,
        //            status = bookingResourceParams.Status,
        //            search = bookingResourceParams.Search
        //        });
        //    case ResourceUriType.NextPage:
        //        return Url.Link("GetBookings", new
        //        {
        //            sortBy = bookingResourceParams.SortBy,
        //            page = bookingResourceParams.Page + 1,
        //            pageSize = bookingResourceParams.PageSize,
        //            status = bookingResourceParams.Status,
        //            search = bookingResourceParams.Search
        //        });
        //    default:
        //        return Url.Link("GetBookings", new
        //        {
        //            sortBy = bookingResourceParams.SortBy,
        //            page = bookingResourceParams.Page,
        //            pageSize = bookingResourceParams.PageSize,
        //            status = bookingResourceParams.Status,
        //            search = bookingResourceParams.Search
        //        });
        //}
    }
}

