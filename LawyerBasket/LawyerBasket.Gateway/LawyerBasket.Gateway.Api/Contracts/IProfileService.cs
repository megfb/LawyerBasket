using LawyerBasket.Gateway.Api.Dtos;
using LawyerBasket.Shared.Common.Response;

namespace LawyerBasket.Gateway.Api.Contracts
{
    public interface IProfileService
    {
        Task<ApiResult<ProfileDto>> GetUserProfileFullAsync();
    }
}

