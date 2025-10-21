using FluentValidation;
using LawyerBasket.ProfileService.Application.Commands;

namespace LawyerBasket.ProfileService.Application.Validators
{
  public class UpdateAddressValidator : AbstractValidator<UpdateAddressCommand>
  {
    public UpdateAddressValidator()
    {
      RuleFor(x => x.AddressLine).NotNull().WithMessage("AddressLine boş olamaz.").NotEmpty().WithMessage("AddressLine boş olamaz.").MaximumLength(250).WithMessage("AddressLine en fazla 250 karakter olabilir.");
    }
  }
}
