using FluentValidation;
using LawyerBasket.PostService.Application.Commands;

namespace LawyerBasket.PostService.Application.Validators
{
  public class CreatePostValidator : AbstractValidator<CreatePostCommand>
  {
    public CreatePostValidator()
    {
      RuleFor(x => x.Content).NotNull()
          .NotEmpty().WithMessage("Can't be null")
          .MaximumLength(255).WithMessage("Email must not exceed 255 characters");
    }
  }
}
