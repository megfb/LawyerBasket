using LawyerBasket.PostService.Application.Dtos;
using LawyerBasket.Shared.Common.Response;
using MediatR;

namespace LawyerBasket.PostService.Application.Commands
{
  public class CreateLikesCommand : IRequest<ApiResult<LikesDto>>
  {
    public string UserId { get; set; } = default!;
    public string PostId { get; set; } = default!;
  }
}
