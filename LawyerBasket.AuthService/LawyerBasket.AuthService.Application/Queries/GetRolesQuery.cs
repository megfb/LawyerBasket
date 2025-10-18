using LawyerBasket.AuthService.Application.Dtos;
using MediatR;

namespace LawyerBasket.AuthService.Application.Queries
{
  public class GetRolesQuery : IRequest<ApiResult<List<AppRoleDto>>>
  {
  }
}
