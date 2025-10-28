using LawyerBasket.PostService.Application.Dtos;
using MediatR;

namespace LawyerBasket.PostService.Application.Queries
{
  public class GetPostsQuery : IRequest<ApiResult<IEnumerable<PostDto>>>
  {
    public string Id { get; set; }
  }
}
