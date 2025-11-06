using LawyerBasket.Shared.Common.Response;
using MediatR;

namespace LawyerBasket.AuthService.Application.Commands
{
  public class RemoveRoleCommand : IRequest<ApiResult>
  {
    public string Id { get; set; } = default!;
  }
}
