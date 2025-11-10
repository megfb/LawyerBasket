using FluentValidation;
using LawyerBasket.ProfileService.Application.Commands;

namespace LawyerBasket.ProfileService.Application.Validators
{
    public class CreateCertificateValidator : AbstractValidator<CreateCertificateCommand>
    {
        public CreateCertificateValidator()
        {

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Sertifika adı boş olamaz.")
                .MaximumLength(150).WithMessage("Sertifika adı en fazla 150 karakter olabilir.");

            RuleFor(x => x.Institution)
                .NotEmpty().WithMessage("Sertifikayı veren kurum boş olamaz.")
                .MaximumLength(150).WithMessage("Kurum adı en fazla 150 karakter olabilir.");

            RuleFor(x => x.DateReceived)
                .NotEmpty().WithMessage("Sertifika alınma tarihi boş olamaz.")
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Sertifika tarihi bugünden ileri olamaz.");

        }
    }
}
