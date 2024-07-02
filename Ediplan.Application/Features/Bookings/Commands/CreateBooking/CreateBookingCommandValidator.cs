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
    public CreateBookingCommandValidator(IBookingRepository bookingRepository)
    {
        _bookingRepository = bookingRepository;

        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(50).WithMessage("{ProperyName} must not exceed 50 characters.");

        RuleFor(p => p.StartDate)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .GreaterThan(DateTime.Now);

        RuleFor(b => b)
            .MustAsync(BookingNameAndDateUnique)
            .WithMessage("A booking with the same name and start date already exists");
    }

    private async Task<bool> BookingNameAndDateUnique(CreateBookingCommand booking, CancellationToken cancellationToken)
    {
        return !(await _bookingRepository.IsBookingNameAndDateUnique(booking.Name, booking.StartDate));
    }
}
