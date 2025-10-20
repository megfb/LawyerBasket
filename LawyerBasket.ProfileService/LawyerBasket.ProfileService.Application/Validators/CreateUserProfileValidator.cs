using FluentValidation;
using LawyerBasket.ProfileService.Application.Commands;

namespace LawyerBasket.ProfileService.Application.Validators
{
  public class CreateUserProfileValidator : AbstractValidator<CreateUserProfileCommand>
  {
    public CreateUserProfileValidator()
    {
      RuleFor(x => x.FirstName)
          .NotEmpty().WithMessage("First name is required")
          .MaximumLength(100).WithMessage("First name cannot exceed 100 characters");

      RuleFor(x => x.LastName)
          .NotEmpty().WithMessage("Last name is required")
          .MaximumLength(100).WithMessage("Last name cannot exceed 100 characters");

      RuleFor(x => x.Email)
          .NotEmpty().WithMessage("Email is required")
          .EmailAddress().WithMessage("Invalid email format")
          .MaximumLength(200).WithMessage("Email cannot exceed 200 characters");

      RuleFor(x => x.PhoneNumber)
          .Matches(@"^\+?\d{10,15}$")
          .When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber))
          .WithMessage("Invalid phone number format (must be 10–15 digits, optional +)");

      RuleFor(x => x.GenderId)
          .NotNull().WithMessage("Gender is required");

      RuleFor(x => x.BirthDate)
          .LessThan(DateTime.UtcNow)
          .WithMessage("Birth date cannot be in the future")
          .When(x => x.BirthDate.HasValue);

      RuleFor(x => x.NationalId)
          .MaximumLength(50).WithMessage("National ID cannot exceed 50 characters")
          .Matches(@"^\d{11}$").WithMessage("National ID must be 11 digits")
          .When(x => !string.IsNullOrWhiteSpace(x.NationalId));

    }
  }
}


