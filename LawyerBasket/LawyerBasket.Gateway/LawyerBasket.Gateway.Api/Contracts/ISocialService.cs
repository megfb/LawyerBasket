using LawyerBasket.Gateway.Api.Dtos;
using LawyerBasket.Shared.Common.Response;

namespace LawyerBasket.Gateway.Api.Contracts
{
    public interface ISocialService
    {
        Task<ApiResult<List<FriendshipDto>>> GetUserFriendsAsync();
        Task<ApiResult<List<FriendWithProfileDto>>> GetUserFriendsWithProfilesAsync();
        Task<ApiResult> DeleteFriendshipAsync(string friendshipId);
    }
}

