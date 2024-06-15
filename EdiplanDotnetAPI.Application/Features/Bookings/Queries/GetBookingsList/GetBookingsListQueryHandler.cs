using AutoMapper;
using EdiplanDotnetAPI.Application.Contracts.Persistence;
using EdiplanDotnetAPI.Application.Features.Bookings.Queries.GetBookingDetail;
using EdiplanDotnetAPI.Domain.Entities;
using FluentValidation.Internal;
using MediatR;

namespace EdiplanDotnetAPI.Application.Features.Bookings.Queries.GetBookingsList;

public class GetBookingsListQueryHandler : IRequestHandler<GetBookingsListQuery, List<BookingListVm>>
{
    private readonly IMapper _mapper;
    private readonly IBookingRepository _bookingRepository;

    public GetBookingsListQueryHandler(IMapper mapper, IBookingRepository bookingRepository)
    {
        _mapper = mapper;
        _bookingRepository = bookingRepository;
    }
    public async Task<List<BookingListVm>> Handle(GetBookingsListQuery request, CancellationToken cancellationToken)
    {
        // List all async, include navigation properties using true statement
        var allBookings = (await _bookingRepository.ListAllAsync(request));
        return _mapper.Map<List<BookingListVm>>(allBookings);
    }
}
