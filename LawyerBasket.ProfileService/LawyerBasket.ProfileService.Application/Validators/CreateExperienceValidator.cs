using FluentValidation;
using LawyerBasket.ProfileService.Application.Commands;

namespace LawyerBasket.ProfileService.Application.Validators
{
  public class CreateExperienceValidator : AbstractValidator<CreateExperienceCommand>
  {
    public CreateExperienceValidator()
    {
      RuleFor(x => x.CompanyName)
          .NotEmpty().WithMessage("Şirket adı zorunludur.")
          .MaximumLength(150).WithMessage("Şirket adı en fazla 150 karakter olabilir.");

      RuleFor(x => x.Position)
          .NotEmpty().WithMessage("Pozisyon alanı boş olamaz.")
          .MaximumLength(100).WithMessage("Pozisyon en fazla 100 karakter olabilir.");

      RuleFor(x => x.StartDate)
          .NotEmpty().WithMessage("Başlangıç tarihi zorunludur.")
          .LessThanOrEqualTo(DateTime.Now).WithMessage("Başlangıç tarihi gelecekte olamaz.");

      RuleFor(x => x.EndDate)
          .GreaterThanOrEqualTo(x => x.StartDate)
          .When(x => x.EndDate.HasValue)
          .WithMessage("Bitiş tarihi başlangıç tarihinden önce olamaz.");

      RuleFor(x => x.Description)
          .MaximumLength(500).WithMessage("Açıklama en fazla 500 karakter olabilir.");
    }
  }
}
