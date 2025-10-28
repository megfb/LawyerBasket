using LawyerBasket.AuthService.Application.Dtos;
using LawyerBasket.Shared.Common.Response;
using MediatR;

namespace LawyerBasket.AuthService.Application.Commands
{
  public class UpdateRoleCommand : IRequest<ApiResult<AppRoleDto>>
  {
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
  }
}
