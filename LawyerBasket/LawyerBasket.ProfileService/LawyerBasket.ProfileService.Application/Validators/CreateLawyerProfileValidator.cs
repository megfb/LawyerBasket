using FluentValidation;
using LawyerBasket.ProfileService.Application.Commands;

namespace LawyerBasket.ProfileService.Application.Validators
{
    public class CreateLawyerProfileValidator : AbstractValidator<CreateLawyerProfileCommand>
    {
        public CreateLawyerProfileValidator()
        {
            RuleFor(x => x.BarAssociation)
                .NotEmpty().WithMessage("BarAssociation boş olamaz.")
                .MaximumLength(150).WithMessage("BarAssociation en fazla 150 karakter olabilir.");

            RuleFor(x => x.BarNumber)
                .NotEmpty().WithMessage("BarNumber boş olamaz.")
                .MaximumLength(50).WithMessage("BarNumber en fazla 50 karakter olabilir.");

            RuleFor(x => x.LicenseNumber)
                .NotEmpty().WithMessage("LicenseNumber boş olamaz.")
                .MaximumLength(50).WithMessage("LicenseNumber en fazla 50 karakter olabilir.");

            RuleFor(x => x.LicenseDate)
                .NotEmpty().WithMessage("LicenseDate boş olamaz.")
                .LessThanOrEqualTo(DateTime.Today).WithMessage("LicenseDate bugünden büyük olamaz.");

            RuleFor(x => x.About)
                .MaximumLength(2000).WithMessage("Hakkında yazısı en fazla 2000 karakter olabilir.")
                .When(x => !string.IsNullOrEmpty(x.About));
        }
    }
}
