using FluentValidation;
using LawyerBasket.ProfileService.Application.Commands;

namespace LawyerBasket.ProfileService.Application.Validators
{
    public class UpdateCityValidator : AbstractValidator<UpdateCityCommand>
    {
        public UpdateCityValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("City Id zorunludur.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Şehir adı zorunludur.")
                .MaximumLength(200).WithMessage("Şehir adı en fazla 200 karakter olabilir.");
        }
    }
}

