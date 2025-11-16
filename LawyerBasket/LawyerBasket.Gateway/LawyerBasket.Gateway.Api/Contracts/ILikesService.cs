using LawyerBasket.Gateway.Api.Dtos;
using LawyerBasket.Shared.Common.Response;

namespace LawyerBasket.Gateway.Api.Contracts
{
    public interface ILikesService
    {
        Task<ApiResult<List<PostDto>>> GetPostsLikedByUserAsync();
        Task<ApiResult<List<PostLikeUserDto>>> GetPostLikesWithUsersAsync(string postId);
    }
}

