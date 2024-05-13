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
public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, CreateBookingCommandResponse>
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

    public async Task<CreateBookingCommandResponse> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
    {
        // Create custom response
        var createBookingCommandResponse = new CreateBookingCommandResponse();

        // old code:
        // var booking = _mapper.Map<Booking>(request);

        // Create validator with injected repository to include custom rules
        var validator = new CreateBookingCommandValidator(_bookingRepository);
        var validationResult = await validator.ValidateAsync(request);

        // If validation fails
        if(validationResult.Errors.Count > 0)
        {
            createBookingCommandResponse.Success = false;
            createBookingCommandResponse.ValidationErrors = new List<string>();
            foreach (var error in validationResult.Errors)
            {
                createBookingCommandResponse.ValidationErrors.Add(error.ErrorMessage);
            }
        }

        // If validation successful
        if(createBookingCommandResponse.Success)
        {
            // Create new entity
            var booking = _mapper.Map<Booking>(request);

            // Add entity to DB
            booking = await _bookingRepository.AddAsync(booking);

            // Add DTO to response
            createBookingCommandResponse.Booking = _mapper.Map<CreateBookingDto>(booking);
        
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

        }

        return createBookingCommandResponse;
    }
}
