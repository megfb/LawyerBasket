using FluentValidation;
using LawyerBasket.SocialService.Api.Domain.Entities;

namespace LawyerBasket.SocialService.Api.Application.Validators
{
  public class FriendConnectionValidator : AbstractValidator<FriendConnection>
  {
    public FriendConnectionValidator()
    {

    }
  }
}
