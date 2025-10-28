using LawyerBasket.Shared.Common.Response;
using MediatR;

namespace LawyerBasket.SocialService.Api.Application.Commands
{
  public class CreateFriendConnectionCommand : IRequest<ApiResult>
  {
    public string SenderId { get; set; }
    public string ReceiverId { get; set; }

  }
}
