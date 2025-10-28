using LawyerBasket.PostService.Application.Dtos;
using MediatR;

namespace LawyerBasket.PostService.Application.Commands
{
  public class CreatePostCommand : IRequest<ApiResult<PostDto>>
  {
    public string UserId { get; set; }
    public string Content { get; set; }
  }
}
