using Ediplan.Application.Contracts.Infrastructure;
using Ediplan.Application.Contracts.Persistence;
using Ediplan.Application.Exceptions;
using Ediplan.Application.Features.Bookings.Commands.CreateBooking;
using Ediplan.Application.Models.Mail;
using Ediplan.Domain.Entities;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace Ediplan.Application.Features.Bookings;

public class BookingHandlerUtils<T>
{
    private readonly IEmailService _emailService;
    private readonly IBookingRepository _bookingRepository;
    private readonly IAssetRepository _assetRepository;
    private readonly IBookingGroupRepository _bookingGroupRepository;
    private readonly ILogger<T> _logger;

    public BookingHandlerUtils(IBookingRepository bookingRepository, IAssetRepository assetRepository, IBookingGroupRepository bookingGroupRepository, IEmailService emailService, ILogger<T> logger)
    {
        _bookingRepository = bookingRepository;
        _assetRepository = assetRepository;
        _bookingGroupRepository = bookingGroupRepository;
        _emailService = emailService;
        _logger = logger;
    }

    public async Task<ValidationResult> HandleValidation(CreateBookingCommand request, CreateBookingCommandResponse? response)
    {
        var validator = new CreateBookingCommandValidator(
            _bookingRepository,
            _assetRepository,
            _bookingGroupRepository
            );

        var validationResult = await validator.ValidateAsync(request);

        if (validationResult.Errors.Any())
        {
            if (response is not null)
            {
                response.Success = false;
                response.ValidationErrors = new List<string>();
                foreach (var error in validationResult.Errors)
                {
                    response.ValidationErrors.Add(error.ErrorMessage);
                } 
            }

            _logger.LogWarning("Validation failed for booking creation." + validationResult);
        }

        return validationResult;
    }

    public async Task SendEmail(CreateBookingCommand request, Booking booking)
    {
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
}