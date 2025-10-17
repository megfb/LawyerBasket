using FluentValidation;
using LawyerBasket.AuthService.Application.Queries;

namespace LawyerBasket.AuthService.Application.Validators
{
    public class LoginQueryValidator : AbstractValidator<LoginQuery>
    {
        public LoginQueryValidator()
        {
            RuleFor(x => x.Email)
             .NotEmpty().WithMessage("Email cannot be empty")
             .NotNull().WithMessage("Email cannot be null")
             .MaximumLength(100).WithMessage("Email must not exceed 100 characters")
             .EmailAddress().WithMessage("Email must be a valid email address")
             .Must(email => email.Contains("@"))
             .WithMessage("Email must contain '@' character");

            RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password cannot be empty")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long")
            .MaximumLength(12).WithMessage("Password must not exceed 50 characters")
            .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter")
            .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter")
            .Matches("[0-9]").WithMessage("Password must contain at least one number")
            .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character");
        }
    }
}
