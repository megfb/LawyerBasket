using LawyerBasket.Gateway.Api.Dtos;
using LawyerBasket.Shared.Common.Response;

namespace LawyerBasket.Gateway.Api.Contracts
{
    public interface ICommentService
    {
        Task<ApiResult<List<PostDto>>> GetPostsCommentedByUserAsync();
    }
}

