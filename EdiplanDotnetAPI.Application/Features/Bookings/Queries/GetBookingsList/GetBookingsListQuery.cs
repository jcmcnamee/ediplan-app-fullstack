using EdiplanDotnetAPI.Application.Helpers;
using MediatR;

namespace EdiplanDotnetAPI.Application.Features.Bookings.Queries.GetBookingsList;

public class GetBookingsListQuery : IRequest<PagedList<BookingListVm>>
{
    const int maxPageSize = 20;
    public string? Status{ get; set; }

    public string? Search { get; set; }
    public string OrderBy { get; set; } = "StartDate";

    public string? Fields { get; set; }

    public int Page { get; set; } = 1;

    private int _pageSize = 5;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > maxPageSize) ? maxPageSize : value;
    }
}
