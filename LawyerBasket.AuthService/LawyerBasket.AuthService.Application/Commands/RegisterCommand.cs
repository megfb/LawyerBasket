using LawyerBasket.AuthService.Application.Dtos;
using LawyerBasket.Shared.Common.Response;
using MediatR;

namespace LawyerBasket.AuthService.Application.Commands
{
  public class RegisterCommand : IRequest<ApiResult<AppUserDto>>
  {
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
  }
}
