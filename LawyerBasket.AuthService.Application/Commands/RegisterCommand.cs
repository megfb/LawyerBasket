using LawyerBasket.AuthService.Application.Dtos;
using MediatR;

namespace LawyerBasket.AuthService.Application.Commands
{
    public class RegisterCommand : IRequest<ApiResult<AppUserDto>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
