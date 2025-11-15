using LawyerBasket.Gateway.Api.Dtos;

namespace LawyerBasket.Gateway.Api.Services
{
    public interface IProfileService
    {
        Task<ProfileDto> GetUserProfileFullAsync();
    }
}

