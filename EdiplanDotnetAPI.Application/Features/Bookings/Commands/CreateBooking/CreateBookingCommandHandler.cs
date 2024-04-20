using AutoMapper;
using EdiplanDotnetAPI.Application.Contracts.Infrastructure;
using EdiplanDotnetAPI.Application.Contracts.Persistence;
using EdiplanDotnetAPI.Application.Models.Mail;
using EdiplanDotnetAPI.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdiplanDotnetAPI.Application.Features.Bookings.Commands.CreateBooking;
internal class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, Guid>
{
    private readonly IMapper _mapper;
    private readonly IBookingRepository _bookingRepository;
    private readonly IEmailService _emailService;

    public CreateBookingCommandHandler(IMapper mapper, IBookingRepository bookingRepository, IEmailService emailService)
    {
        _emailService = emailService;
        _mapper = mapper;
        _bookingRepository = bookingRepository;
    }
    public async Task<Guid> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
    {
        var booking = _mapper.Map<Booking>(request);

        var validator = new CreateBookingCommandValidator(_bookingRepository);
        var validationResult = await validator.ValidateAsync(request);

        if (validationResult.Errors.Count > 0)
            throw new Exceptions.ValidationException(validationResult);

        booking = await _bookingRepository.AddAsync(booking);

        // Send email notification
        var email = new Email()
        {
            To = "jcmcnamee@hotmail.com",
            Subject = "A new booking was created",
            Body = $"A new booking was created: {request}"
        };

        try
        {
            await _emailService.SendEmail(email);
        }
        catch (Exception ex)
        {
            // Log
        }

        return booking.Id;
    }
}
