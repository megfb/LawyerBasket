using LawyerBasket.AuthService.Application.Dtos;
using MediatR;

namespace LawyerBasket.AuthService.Application.Commands
{
    public class CreateRoleCommand : IRequest<ApiResult<AppRoleDto>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
