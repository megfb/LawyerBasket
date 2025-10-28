using LawyerBasket.Shared.Common.Response;
using MediatR;

namespace LawyerBasket.PostService.Application.Commands
{
  public class RemovePostCommand : IRequest<ApiResult>
  {
    public string Id { get; set; }
  }
}
