using LawyerBasket.PostService.Application.Dtos;
using MediatR;

namespace LawyerBasket.PostService.Application.Queries
{
  public class GetPostQuery : IRequest<ApiResult<PostDto>>
  {
    public string Id { get; set; }
  }
}
