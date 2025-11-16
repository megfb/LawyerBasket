using LawyerBasket.Gateway.Api.Dtos;
using LawyerBasket.Shared.Common.Response;

namespace LawyerBasket.Gateway.Api.Contracts
{
    public interface IPostService
    {
        Task<ApiResult<List<PostDto>>> GetUserPostsAsync();
    }
}

