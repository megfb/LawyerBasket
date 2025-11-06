using FluentValidation;
using LawyerBasket.AuthService.Application.Commands;

namespace LawyerBasket.AuthService.Application.Validators
{
    public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
    {
        public CreateRoleCommandValidator()
        {
            RuleFor(x => x.Name)
             .NotEmpty().WithMessage("Role name cannot be empty")
             .NotNull().WithMessage("Role name cannot be null")
             .MaximumLength(50).WithMessage("Role name must not exceed 50 characters");
            RuleFor(x => x.Description)
             .Length(0, 200).WithMessage("Description must not exceed 200 characters")
             .NotEmpty().WithMessage("Description name cannot be empty")
             .NotNull().WithMessage("Description name cannot be null");
        }
    }
}
