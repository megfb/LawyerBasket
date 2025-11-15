using FluentValidation;
using LawyerBasket.ProfileService.Application.Commands;

namespace LawyerBasket.ProfileService.Application.Validators
{
    public class CreateGenderValidator : AbstractValidator<CreateGenderCommand>
    {
        public CreateGenderValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Cinsiyet adı zorunludur.")
                .MaximumLength(64).WithMessage("Cinsiyet adı en fazla 64 karakter olabilir.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Açıklama zorunludur.")
                .MaximumLength(256).WithMessage("Açıklama en fazla 256 karakter olabilir.");
        }
    }
}

