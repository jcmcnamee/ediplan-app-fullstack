using Ediplan.Application.Contracts.Persistence;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ediplan.Application.Features.Bookings.Commands.CreateBooking;
public class CreateBookingCommandValidator : AbstractValidator<CreateBookingCommand>
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IAssetRepository _assetRepository;
    private readonly IBookingGroupRepository _bookingGroupRepository;

    public CreateBookingCommandValidator(IBookingRepository bookingRepository, IAssetRepository assetRepository, IBookingGroupRepository bookingGrouprepository)
    {
        _bookingRepository = bookingRepository;
        _assetRepository = assetRepository;
        _bookingGroupRepository = bookingGrouprepository;

        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(50).WithMessage("{ProperyName} must not exceed 50 characters.");

        RuleFor(p => p.StartDate)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .GreaterThan(DateTime.Now);

        RuleFor(p => p.EndDate)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .GreaterThan(p => p.StartDate)
            .WithMessage("End date must be greater than start date.");

        RuleFor(b => b)
            .MustAsync(BookingNameAndDateUnique)
            .WithMessage("A booking with the same name and start date already exists");

        RuleFor(b => b)
            .MustAsync(DoAllAssetsExist)
            .WithMessage("One or more assets were not found")
            .When(b => b.AssetIds?.Any() == true);

        RuleFor(b => b)
            .MustAsync(DoAllBookingGroupsExist)
            .WithMessage("One or more booking groups were not found")
            .When(b => b.BookingGroupIds?.Any() == true);
    }

    private async Task<bool> BookingNameAndDateUnique(CreateBookingCommand booking, CancellationToken cancellationToken)
    {
        return !(await _bookingRepository.IsBookingNameAndDateUnique(booking.Name, booking.StartDate));
    }

    private async Task<bool> DoAllAssetsExist(CreateBookingCommand booking, CancellationToken cancellationToken)
    {
        var distinctAssetIds = booking.AssetIds!.Distinct().ToList();
        var assets = await _assetRepository.GetAssetsByIdsAsync(distinctAssetIds);
        return assets.Count() == distinctAssetIds.Count();
    }

    private async Task<bool> DoAllBookingGroupsExist(CreateBookingCommand booking, CancellationToken cancellationToken)
    {
        var distinctBookingGroupIds = booking.BookingGroupIds!.Distinct().ToList();
        var bookingGroups = await _bookingGroupRepository.GetGroupsByIdsAsync(distinctBookingGroupIds);
        return bookingGroups.Count() == distinctBookingGroupIds.Count();
    }
    
}
