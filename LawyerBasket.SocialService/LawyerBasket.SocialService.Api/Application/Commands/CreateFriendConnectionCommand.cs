using LawyerBasket.ProfileService.Application;
using LawyerBasket.SocialService.Api.Domain.Entities;
using MediatR;

namespace LawyerBasket.SocialService.Api.Application.Commands
{
  public class CreateFriendConnectionCommand:IRequest<ApiResult>
  {
    public string SenderId { get; set; }
    public string ReceiverId { get; set; }

  }
}
