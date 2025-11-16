using LawyerBasket.Gateway.Api.Dtos;
using LawyerBasket.Shared.Common.Response;

namespace LawyerBasket.Gateway.Api.Contracts
{
    public interface IUserProfileService
    {
        Task<ApiResult<UserProfileWDetailsDto>> GetUserProfileFullAsync();
        Task<ApiResult<List<UserProfileDto>>> GetUserProfilesByIdsAsync(List<string> ids);
    }
}

