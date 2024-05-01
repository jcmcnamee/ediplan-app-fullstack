using AutoMapper;
using EdiplanDotnetAPI.Application.Contracts.Persistence;
using EdiplanDotnetAPI.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdiplanDotnetAPI.Application.Features.Bookings.Commands.DeleteBooking;
public class DeleteBookingCommandHandler : IRequestHandler<DeleteBookingCommand>
{
    private readonly IMapper _mapper;
    private readonly IAsyncRepository<Booking> _bookingRepository;

    public DeleteBookingCommandHandler(IMapper mapper, IAsyncRepository<Booking> bookingRepository)
    {
        _mapper = mapper;
        _bookingRepository = bookingRepository;
    }
    public async Task Handle(DeleteBookingCommand request, CancellationToken cancellationToken)
    {
        var bookingToDelete = await _bookingRepository.GetByIdAsync(request.Id);
        await _bookingRepository.DeleteAsync(bookingToDelete);
    }
}
