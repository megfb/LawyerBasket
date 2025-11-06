using LawyerBasket.AuthService.Application.Dtos;
using LawyerBasket.Shared.Common.Response;
using MediatR;

namespace LawyerBasket.AuthService.Application.Commands
{
  public class CreateRoleCommand : IRequest<ApiResult<AppRoleDto>>
  {
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
  }
}
