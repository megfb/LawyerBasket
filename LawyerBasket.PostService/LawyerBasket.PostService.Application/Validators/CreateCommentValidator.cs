using FluentValidation;
using LawyerBasket.PostService.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
