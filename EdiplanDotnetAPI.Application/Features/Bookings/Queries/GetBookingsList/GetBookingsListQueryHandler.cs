using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EdiplanDotnetAPI.Application.Contracts.Persistence;
using EdiplanDotnetAPI.Domain.Entities;
using MediatR;

namespace EdiplanDotnetAPI.Application.Features.Bookings.Queries.GetBookingsList;

public class GetBookingsListQueryHandler : IRequestHandler<GetBookingsListQuery, List<BookingListVm>>
{
    private readonly IMapper _mapper;
    private readonly IAsyncRepository<Booking> _bookingRepository;

    public GetBookingsListQueryHandler(IMapper mapper, IAsyncRepository<Booking> bookingRepository)
    {
        _mapper = mapper;
        _bookingRepository = bookingRepository;
    }
    public async Task<List<BookingListVm>> Handle(GetBookingsListQuery request, CancellationToken cancellationToken)
    {
        var allBookings = (await _bookingRepository.ListAllAsync()).OrderBy(x => x.StartDate);
        return _mapper.Map<List<BookingListVm>>(allBookings);
    }
}
