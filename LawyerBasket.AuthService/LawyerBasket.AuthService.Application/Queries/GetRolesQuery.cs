using LawyerBasket.AuthService.Application.Dtos;
using LawyerBasket.Shared.Common.Response;
using MediatR;

namespace LawyerBasket.AuthService.Application.Queries
{
  public class GetRolesQuery : IRequest<ApiResult<List<AppRoleDto>>>
  {
  }
}
