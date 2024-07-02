using AutoMapper;
using Ediplan.Application.Contracts.Persistence;
using Ediplan.Application.Exceptions;
using Ediplan.Application.Features.Bookings.Commands.UpdateBooking;
using Ediplan.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ediplan.Application.Features.Bookings.Commands.PatchBooking;
public class PatchBookingCommandHandler : IRequestHandler<PatchBookingCommand>
{
    private readonly IMapper _mapper;
    private readonly IBookingRepository _bookingRepository;

    public PatchBookingCommandHandler(IMapper mapper, IBookingRepository bookingRepository)
    {
        _mapper = mapper;
        _bookingRepository = bookingRepository;
    }

    public async Task Handle(PatchBookingCommand request, CancellationToken cancellationToken)
    {
        var booking = await _bookingRepository.GetByIdAsync(request.Id);

        if(booking == null)
        {
            throw new NotFoundException(nameof(Booking), request.Id);
        }

        var bookingToPatch = _mapper.Map<UpdateBookingDto>(booking);

        // Create new patch doc and 
        //var bookingPatch = new JsonPatchDocument<UpdateBookingDto>();
        //foreach (var opr in request.Booking.Operations)
        //{
        //    bookingPatch.Operations.Add(new Operation<UpdateBookingDto>
        //    {
        //        op = opr.op,
        //        path = opr.path,
        //        value = opr.value
        //    });
        //}

        //bookingPatch.ContractResolver = request.Booking.ContractResolver;
        request.Booking.ApplyTo(bookingToPatch);

        _mapper.Map(bookingToPatch, booking);

        await _bookingRepository.UpdateAsync(booking);
    }
}
