using LawyerBasket.AuthService.Application.Dtos;
using LawyerBasket.AuthService.Domain.Entities;

namespace LawyerBasket.AuthService.Application.Contracts.Infrastructure
{
    public interface ITokenService
    {
        public TokenDto CreateToken(AppUser user);

    }
}
