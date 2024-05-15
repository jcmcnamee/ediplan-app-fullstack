using AutoMapper;
using EdiplanDotnetAPI.Application.Contracts.Infrastructure;
using EdiplanDotnetAPI.Application.Contracts.Persistence;
using EdiplanDotnetAPI.Application.Exceptions;
using EdiplanDotnetAPI.Application.Models.Mail;
using EdiplanDotnetAPI.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EdiplanDotnetAPI.Application.Features.Bookings.Commands.CreateBooking;
public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, CreateBookingCommandResponse>
{
    private readonly IMapper _mapper;
    private readonly IBookingRepository _bookingRepository;
    private readonly IEmailService _emailService;
    private readonly ILogger<CreateBookingCommandHandler> _logger;

    public CreateBookingCommandHandler(IMapper mapper, IBookingRepository bookingRepository, IEmailService emailService, ILogger<CreateBookingCommandHandler> logger)
    {
        _emailService = emailService;
        _logger = logger;
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

            throw new ValidationException(validationResult);
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
                _logger.LogError($"Mail about booking {booking.Id} failed due to an error with the mail service: {ex.Message}");
            }

        }

        return createBookingCommandResponse;
    }
}
