using LawyerBasket.PostService.Application.Dtos;
using LawyerBasket.Shared.Common.Response;
using MediatR;

namespace LawyerBasket.PostService.Application.Commands
{
  public class CreatePostCommand : IRequest<ApiResult<PostDto>>
  {
    public string UserId { get; set; } = default!;
    public string Content { get; set; } = default!;
  }
}
