using LawyerBasket.AuthService.Application.Dtos;
using MediatR;

namespace LawyerBasket.AuthService.Application.Commands
{
    public class ChangePasswordCommand : IRequest<ApiResult<AppUserDto>>
    {
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string ReNewPassword { get; set; }
    }
}
