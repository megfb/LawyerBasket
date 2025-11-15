using LawyerBasket.Gateway.Api.Dtos;

namespace LawyerBasket.Gateway.Api.Services
{
    public interface IUserProfileService
    {
        Task<UserProfileWDetailsDto?> GetUserProfileFullAsync();
    }
}

