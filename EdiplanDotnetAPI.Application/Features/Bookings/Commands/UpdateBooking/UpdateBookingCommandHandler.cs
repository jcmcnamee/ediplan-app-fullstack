using AutoMapper;
using EdiplanDotnetAPI.Application.Contracts.Persistence;
using EdiplanDotnetAPI.Application.Exceptions;
using EdiplanDotnetAPI.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdiplanDotnetAPI.Application.Features.Bookings.Commands.UpdateBooking;
public class UpdateBookingCommandHandler : IRequestHandler<UpdateBookingCommand>
{
    private readonly IMapper _mapper;
    private readonly IAsyncRepository<Booking> _bookingRepository;

    public UpdateBookingCommandHandler(IMapper mapper, IAsyncRepository<Booking> bookingRepository)
    {
        _mapper = mapper;
        _bookingRepository = bookingRepository;
    }
    public async Task Handle(UpdateBookingCommand request, CancellationToken cancellationToken)
    {
        var bookingToUpdate = await _bookingRepository.GetByIdAsync(request.Id);

        if(bookingToUpdate == null)
        {
            throw new NotFoundException(nameof(Booking), request.Id);
        }

        _mapper.Map(request, bookingToUpdate, typeof(UpdateBookingCommand), typeof(Booking));

        await _bookingRepository.UpdateAsync(bookingToUpdate);
    }
}
