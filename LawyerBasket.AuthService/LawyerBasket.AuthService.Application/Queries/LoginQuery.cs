using LawyerBasket.AuthService.Application.Dtos;
using LawyerBasket.Shared.Common.Response;
using MediatR;

namespace LawyerBasket.AuthService.Application.Queries
{
    public class LoginQuery : IRequest<ApiResult<TokenDto>>
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
