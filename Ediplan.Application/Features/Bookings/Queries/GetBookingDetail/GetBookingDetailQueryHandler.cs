using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ediplan.Application.Contracts.Persistence;
using Ediplan.Application.Exceptions;
using Ediplan.Domain.Entities;
using MediatR;

namespace Ediplan.Application.Features.Bookings.Queries.GetBookingDetail;

public class GetBookingDetailQueryHandler : IRequestHandler<GetBookingDetailQuery, BookingDetailVm>
{
    private readonly IMapper _mapper;
    private readonly IAsyncRepository<Booking> _bookingRepo;
    private readonly IAsyncRepository<Production> _productionRepo;
    private readonly IAsyncRepository<Location> _locationRepo;
    private readonly IAsyncRepository<Person> _personRepo;

    public GetBookingDetailQueryHandler(
        IMapper mapper,
        IAsyncRepository<Booking> bookingRepo,
        IAsyncRepository<Production> productionRepo,
        IAsyncRepository<Location> locationRepo,
        IAsyncRepository<Person> personRepo)
    {
        _mapper = mapper;
        _bookingRepo = bookingRepo;
        _productionRepo = productionRepo;
        _locationRepo = locationRepo;
        _personRepo = personRepo;
    }

    public async Task<BookingDetailVm> Handle(GetBookingDetailQuery request, CancellationToken cancellationToken)
    {
        var booking = await _bookingRepo.GetByIdAsync(request.Id);

        if(booking == null)
        {
            throw new NotFoundException(nameof(Booking), request.Id);
        }

        var bookingDetailDto = _mapper.Map<BookingDetailVm>(booking);

        if (booking.ProductionId.HasValue)
        {
            var production = await _productionRepo.GetByIdAsync(booking.ProductionId.Value);
            bookingDetailDto.Production = _mapper.Map<ProductionDto>(production);
        }

        return bookingDetailDto;
    }
}
