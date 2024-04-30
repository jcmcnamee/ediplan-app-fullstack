using FluentValidation;

namespace EdiplanDotnetAPI.Application.Features.BookingGroups.Commands.CreateBookingGroup;
internal class CreateBookingGroupValidator : AbstractValidator<CreateBookingGroupCommand>
{
    public CreateBookingGroupValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(50).WithMessage("{PropertyName} must not exceed 10 characters.");
    }
}
