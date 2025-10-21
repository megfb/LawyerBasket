using FluentValidation;
using LawyerBasket.ProfileService.Application.Commands;

namespace LawyerBasket.ProfileService.Application.Validators
{
  public class CreateContactValidator : AbstractValidator<CreateContactCommand>
  {
    public CreateContactValidator()
    {
      RuleFor(x => x.PhoneNumber)
          .NotEmpty().WithMessage("PhoneNumber boş olamaz.")
          .MaximumLength(20).WithMessage("PhoneNumber en fazla 20 karakter olabilir.");

      RuleFor(x => x.AlternatePhoneNumber)
          .MaximumLength(20).WithMessage("AlternatePhoneNumber en fazla 20 karakter olabilir.");

      RuleFor(x => x.Email)
          .NotEmpty().WithMessage("Email boş olamaz.")
          .EmailAddress().WithMessage("Email geçerli bir email adresi olmalıdır.");

      RuleFor(x => x.AlternateEmail)
          .EmailAddress().WithMessage("AlternateEmail geçerli bir email adresi olmalıdır.");

      RuleFor(x => x.Website)
          .MaximumLength(100).WithMessage("Website en fazla 100 karakter olabilir.");
    }
  }
}
