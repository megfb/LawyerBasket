using FluentValidation;
using LawyerBasket.ProfileService.Application.Commands;

namespace LawyerBasket.ProfileService.Application.Validators
{
    public class UpdateExpertisementValidator : AbstractValidator<UpdateExpertisementCommand>
    {
        public UpdateExpertisementValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Expertisement Id zorunludur.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Uzmanlık alanı adı zorunludur.")
                .MaximumLength(256).WithMessage("Uzmanlık alanı adı en fazla 256 karakter olabilir.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Açıklama zorunludur.")
                .MaximumLength(256).WithMessage("Açıklama en fazla 256 karakter olabilir.");
        }
    }
}

