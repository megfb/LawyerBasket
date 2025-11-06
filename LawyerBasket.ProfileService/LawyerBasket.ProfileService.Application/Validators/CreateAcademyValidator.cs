using FluentValidation;
using LawyerBasket.ProfileService.Application.Commands;

namespace LawyerBasket.ProfileService.Application.Validators
{
    public class CreateAcademyValidator : AbstractValidator<CreateAcademyCommand>
    {
        public CreateAcademyValidator()
        {
            RuleFor(x => x.University)
                .NotEmpty().WithMessage("Üniversite adı zorunludur.")
                .MaximumLength(150).WithMessage("Üniversite adı en fazla 150 karakter olabilir.");

            RuleFor(x => x.Degree)
                .MaximumLength(100).WithMessage("Derece bilgisi en fazla 100 karakter olabilir.")
                .When(x => !string.IsNullOrEmpty(x.Degree));

            RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage("Başlangıç tarihi zorunludur.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Başlangıç tarihi gelecekte olamaz.");

            RuleFor(x => x.EndDate)
                .GreaterThanOrEqualTo(x => x.StartDate)
                .When(x => x.EndDate.HasValue)
                .WithMessage("Bitiş tarihi başlangıç tarihinden önce olamaz.");
        }
    }
}
