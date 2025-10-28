using LawyerBasket.Shared.Common.Response;
using MediatR;

namespace LawyerBasket.PostService.Application.Commands
{
  public class RemoveLikesCommand : IRequest<ApiResult>
  {
    public string PostId { get; set; }
    public string LikeId { get; set; }
  }
}
