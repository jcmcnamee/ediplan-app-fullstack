using AutoMapper;
using EdiplanDotnetAPI.Application.Contracts.Persistence;
using EdiplanDotnetAPI.Application.Features.Bookings.Queries.GetBookingDetail;
using EdiplanDotnetAPI.Application.Helpers;
using EdiplanDotnetAPI.Domain.Entities;
using FluentValidation.Internal;
using MediatR;

namespace EdiplanDotnetAPI.Application.Features.Bookings.Queries.GetBookingsList;

public class GetBookingsListQueryHandler : IRequestHandler<GetBookingsListQuery, PagedList<BookingListVm>>
{
    private readonly IMapper _mapper;
    private readonly IBookingRepository _bookingRepository;

    public GetBookingsListQueryHandler(IMapper mapper, IBookingRepository bookingRepository)
    {
        _mapper = mapper;
        _bookingRepository = bookingRepository;
    }
    public async Task<PagedList<BookingListVm>> Handle(GetBookingsListQuery request, CancellationToken cancellationToken)
    {
        var allBookings = (await _bookingRepository.ListAllAsync(request));
        return _mapper.Map<PagedList<BookingListVm>>(allBookings);

    }
}
