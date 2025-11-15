using LawyerBasket.PostService.Application.Dtos;
using LawyerBasket.Shared.Common.Response;
using MediatR;

namespace LawyerBasket.PostService.Application.Queries
{
    public class GetPostsLikedByUserQuery : IRequest<ApiResult<IEnumerable<PostDto>>>
    {
    }
}

