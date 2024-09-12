using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ediplan.Application.Contracts.Persistence;
using Ediplan.Application.Exceptions;
using Ediplan.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ediplan.Application.Features.Bookings.Queries.GetBookingDetail;

public class GetBookingDetailQueryHandler : IRequestHandler<GetBookingDetailQuery, BookingDetailVm>
{
    private readonly IMapper _mapper;
    private readonly IBookingRepository _bookingRepo;
    private readonly IAsyncRepository<Production> _productionRepo;
    private readonly IAsyncRepository<Location> _locationRepo;
    private readonly IAsyncRepository<Person> _personRepo;
    private readonly ILogger<GetBookingDetailQueryHandler> _logger;

    public GetBookingDetailQueryHandler(
        IMapper mapper,
        IBookingRepository bookingRepo,
        IAsyncRepository<Production> productionRepo,
        IAsyncRepository<Location> locationRepo,
        IAsyncRepository<Person> personRepo,
        ILogger<GetBookingDetailQueryHandler> logger)
    {
        _mapper = mapper;
        _bookingRepo = bookingRepo;
        _productionRepo = productionRepo;
        _locationRepo = locationRepo;
        _personRepo = personRepo;
        _logger = logger;
    }

    public async Task<BookingDetailVm> Handle(GetBookingDetailQuery request, CancellationToken cancellationToken)
    {
        var booking = await _bookingRepo.GetBookingDetail(request.Id);

        if(booking == null)
        {
            throw new NotFoundException(nameof(Booking), request.Id);
        }

        _logger.LogInformation("Returning booking: " + booking.ToString());
        var bookingDetailDto = _mapper.Map<BookingDetailVm>(booking);

        if (booking.ProductionId.HasValue)
        {
            var production = await _productionRepo.GetByIdAsync(booking.ProductionId.Value);
            bookingDetailDto.Production = _mapper.Map<ProductionDto>(production);
        }

        return bookingDetailDto;
    }
}
