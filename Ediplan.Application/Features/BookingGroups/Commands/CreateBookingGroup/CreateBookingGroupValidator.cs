using FluentValidation;

namespace Ediplan.Application.Features.BookingGroups.Commands.CreateBookingGroup;
public class CreateBookingGroupValidator : AbstractValidator<CreateBookingGroupCommand>
{
    public CreateBookingGroupValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull().WithMessage("{PropertyName} must not be null")
            .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");
            //.Must(NotContainSpecialChars).WithMessage("{PropertyName} must not contain special characters.");
    }

    //private bool NotContainSpecialChars(string str)
    //{
    //    if (str == null)
    //        return true;

    //    return str.Any(char.IsLetterOrDigit);
    //}
}
