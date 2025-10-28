using FluentValidation;
using LawyerBasket.PostService.Application.Commands;

namespace LawyerBasket.PostService.Application.Validators
{
  public class CreateCommentValidator : AbstractValidator<CreateCommentCommand>
  {
    public CreateCommentValidator()
    {
      RuleFor(x => x.Text).NotNull().NotEmpty().WithMessage("Text can not be empty").MaximumLength(255).WithMessage("Maximum length is 255");
    }
  }
}
