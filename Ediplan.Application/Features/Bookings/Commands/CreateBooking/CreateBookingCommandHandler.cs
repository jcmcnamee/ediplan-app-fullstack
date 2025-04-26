using AutoMapper;
using Ediplan.Application.Contracts.Infrastructure;
using Ediplan.Application.Contracts.Persistence;
using Ediplan.Application.Exceptions;
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
    private IEmailService _emailService;
    private readonly ILogger<CreateBookingCommandHandler> _logger;
    private readonly BookingHandlerUtils<CreateBookingCommandHandler> _handlerUtils;


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

        _handlerUtils = new BookingHandlerUtils<CreateBookingCommandHandler>(bookingRepository, assetRepository, bookingGroupRepository, emailService, logger);
    }

    public async Task<CreateBookingCommandResponse> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
    {

        _logger.LogInformation("Handling request: " + request.ToString());
        var createBookingCommandResponse = new CreateBookingCommandResponse();

        var validationResult = await _handlerUtils.HandleValidation(request, createBookingCommandResponse);

        if (validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation failed for booking creation.");
            throw new ValidationException(validationResult);
        }

        var booking = _mapper.Map<Booking>(request);

        // Add only distinct and available assets
        if (request.AssetIds?.Any() == true)
        {
            var distinctAssetIds = request.AssetIds.Distinct().ToList();
            var availableAssets = await _assetRepository.GetAvailableAssetsById(distinctAssetIds, request.StartDate, request.EndDate);

            if (availableAssets.Count != distinctAssetIds.Count)
            {
                var invalidAssetIds = distinctAssetIds.Except(availableAssets.Select(a => a.Id)).ToList();

                var message = "One or more assets are not available";
                _logger.LogWarning(message);
                createBookingCommandResponse.Message = message;
                createBookingCommandResponse.Success = false;
                createBookingCommandResponse.UnavailableAssets = distinctAssetIds.Except(availableAssets.Select(a => a.Id)).ToList();
            }

            booking.Assets = availableAssets;
        }

        if (request.BookingGroupIds?.Any() == true)
        {
            var distinctBookingGroupIds = request.BookingGroupIds.Distinct().ToList();
            var bookingGroups = await _bookingGroupRepository.GetGroupsByIdsAsync(distinctBookingGroupIds);
            booking.BookingGroups = bookingGroups;
        }

        #region REFACTORED
        // Fetch and add assets
        //if (request.AssetIds?.Any() == true)
        //{
        //    var distinctAssetIds = request.AssetIds.Distinct().ToList();
        //    var assets = await _assetRepository.GetAssetsByIdsAsync(distinctAssetIds);
        //    if (assets.Count != distinctAssetIds.Count)
        //    {
        //        _logger.LogWarning("One or more assets were not found.");
        //        createBookingCommandResponse.Success = false;
        //        createBookingCommandResponse.ValidationErrors.Add("One or more assets were not found.");
        //        return createBookingCommandResponse;
        //    }

        //    booking.Assets = assets;
        //}

        // Fetch and add booking group
        //if (request.BookingGroupIds != null && request.BookingGroupIds.Any())
        //{
        //    var distinctBookingGroupIds = request.BookingGroupIds.Distinct().ToList();
        //    var bookingGroups = await _bookingGroupRepository.GetGroupsByIdsAsync(distinctBookingGroupIds);
        //    _logger.LogInformation("Booking groups: " + bookingGroups.ToString());
        //    if (bookingGroups.Count != distinctBookingGroupIds.Count)
        //    {
        //        _logger.LogWarning("One or more booking groups were not found.");
        //        createBookingCommandResponse.Success = false;
        //        createBookingCommandResponse.ValidationErrors.Add("One or more booking groups were not found.");
        //        return createBookingCommandResponse;
        //    }

        //    booking.BookingGroups = bookingGroups;
        //}

        #endregion

        booking = await _bookingRepository.AddAsync(booking);

        createBookingCommandResponse.Booking = _mapper.Map<CreateBookingDto>(booking);

        await _handlerUtils.SendEmail(request, booking);

        return createBookingCommandResponse;
    }
}

