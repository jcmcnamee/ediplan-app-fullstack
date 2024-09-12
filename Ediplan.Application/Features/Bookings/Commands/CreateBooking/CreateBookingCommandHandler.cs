using AutoMapper;
using Ediplan.Application.Contracts.Infrastructure;
using Ediplan.Application.Contracts.Persistence;
using Ediplan.Application.Exceptions;
using Ediplan.Application.Models.Mail;
using Ediplan.Domain.Common;
using Ediplan.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ediplan.Application.Features.Bookings.Commands.CreateBooking;
public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, CreateBookingCommandResponse>
{
    private readonly IMapper _mapper;
    private readonly IBookingRepository _bookingRepository;
    private readonly IAssetRepository _assetRepository;
    private readonly IBookingGroupRepository _bookingGroupRepository;
    private readonly IEmailService _emailService;
    private readonly ILogger<CreateBookingCommandHandler> _logger;

    public CreateBookingCommandHandler(IMapper mapper, IBookingRepository bookingRepository, IAssetRepository assetRepository, IBookingGroupRepository bookingGroupRepository, IEmailService emailService, ILogger<CreateBookingCommandHandler> logger)
    {
        _emailService = emailService ??
            throw new ArgumentNullException(nameof(emailService));
        _logger = logger ??
            throw new ArgumentNullException(nameof(logger));
        _mapper = mapper ??
            throw new ArgumentNullException(nameof(mapper));
        _bookingRepository = bookingRepository ??
            throw new ArgumentNullException(nameof(bookingRepository));
        _assetRepository = assetRepository ??
            throw new ArgumentNullException(nameof(assetRepository));
        _bookingGroupRepository = bookingGroupRepository ??
            throw new ArgumentNullException(nameof(bookingGroupRepository));
    }

    public async Task<CreateBookingCommandResponse> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling request: " + request.ToString());
        // Create custom response
        var createBookingCommandResponse = new CreateBookingCommandResponse();

        // Create validator with injected repository to include custom rules
        var validator = new CreateBookingCommandValidator(_bookingRepository);
        var validationResult = await validator.ValidateAsync(request);

        // If validation fails
        if (validationResult.Errors.Any())
        {
            createBookingCommandResponse.Success = false;
            createBookingCommandResponse.ValidationErrors = new List<string>();
            foreach (var error in validationResult.Errors)
            {
                createBookingCommandResponse.ValidationErrors.Add(error.ErrorMessage);
            }
            _logger.LogWarning("Validation failed for booking creation." + validationResult);

            throw new ValidationException(validationResult);
        }

        // Create new booking
        var booking = _mapper.Map<Booking>(request);

        // TODO: Refactor the following validation tasks to be contained within the validator.
        // Fetch and add assets
        if (request.AssetIds != null && request.AssetIds.Any())
        {
            var distinctAssetIds = request.AssetIds.Distinct().ToList();
            var assets = await _assetRepository.GetAssetsByIdsAsync(distinctAssetIds);
            if (assets.Count != distinctAssetIds.Count)
            {
                _logger.LogWarning("One or more assets were not found.");
                createBookingCommandResponse.Success = false;
                createBookingCommandResponse.ValidationErrors.Add("One or more assets were not found.");
                return createBookingCommandResponse;
            }

            booking.Assets = assets;
        }

        // Fetch and add booking group
        if (request.BookingGroupIds != null && request.BookingGroupIds.Any())
        {
            var distinctBookingGroupIds = request.BookingGroupIds.Distinct().ToList();
            var bookingGroups = await _bookingGroupRepository.GetGroupsByIdsAsync(distinctBookingGroupIds);
            _logger.LogInformation("Booking groups: " + bookingGroups.ToString());
            if (bookingGroups.Count != distinctBookingGroupIds.Count)
            {
                _logger.LogWarning("One or more booking groups were not found.");
                createBookingCommandResponse.Success = false;
                createBookingCommandResponse.ValidationErrors.Add("One or more booking groups were not found.");
                return createBookingCommandResponse;
            }

            booking.BookingGroups = bookingGroups;
        }

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

        return createBookingCommandResponse;
    }
}

