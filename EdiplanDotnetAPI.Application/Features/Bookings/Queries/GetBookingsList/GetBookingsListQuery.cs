using MediatR;

namespace EdiplanDotnetAPI.Application.Features.Bookings.Queries.GetBookingsList;

public class GetBookingsListQuery : IRequest<List<BookingListVm>>
{
    public string? Status{ get; set; }
    public string? SearchQuery { get; set; }
}
