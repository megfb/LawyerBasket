using FluentValidation;
using LawyerBasket.ProfileService.Application.Commands;

namespace LawyerBasket.ProfileService.Application.Validators
{
    public class UpdateCertificateValidator : AbstractValidator<UpdateCertificateCommand>
    {
        public UpdateCertificateValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Sertifika Id zorunludur.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Sertifika adı boş olamaz.")
                .MaximumLength(150).WithMessage("Sertifika adı en fazla 150 karakter olabilir.");

            RuleFor(x => x.Institution)
                .NotEmpty().WithMessage("Sertifikayı veren kurum boş olamaz.")
                .MaximumLength(150).WithMessage("Kurum adı en fazla 150 karakter olabilir.");

            RuleFor(x => x.DateReceived)
                .NotEmpty().WithMessage("Sertifika alınma tarihi boş olamaz.")
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Sertifika tarihi bugünden ileri olamaz.");

            RuleFor(x => x.Description)
                .MaximumLength(2000).WithMessage("Sertifika eğitim içeriği en fazla 2000 karakter olabilir.")
                .When(x => !string.IsNullOrEmpty(x.Description));
        }
    }
}

