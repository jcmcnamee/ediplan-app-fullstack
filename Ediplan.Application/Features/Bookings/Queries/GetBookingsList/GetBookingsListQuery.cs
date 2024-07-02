using Ediplan.Application.Helpers;
using MediatR;

namespace Ediplan.Application.Features.Bookings.Queries.GetBookingsList;

public class GetBookingsListQuery : IRequest<PagedList<BookingListVm>>
{
    const int maxPageSize = 20;

    // Filters
    public string? Status{ get; set; }

    // Search & Sort
    public string? Search { get; set; }
    public string SortBy { get; set; } = "StartDate";

    // Shaping
    public string? Fields { get; set; }

    // Pagination
    public int Page { get; set; } = 1;

    private int _pageSize = 5;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > maxPageSize) ? maxPageSize : value;
    }
}
