using LawyerBasket.Shared.Common.Response;
using MediatR;

namespace LawyerBasket.SocialService.Api.Application.Commands
{
  public class CreateFriendshipCommand : IRequest<ApiResult>
  {
    public string UserAId { get; set; }
    public string UserBId { get; set; }
  }
}
