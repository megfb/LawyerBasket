using LawyerBasket.Shared.Common.Response;
using MediatR;

namespace LawyerBasket.ProfileService.Application.Commands
{
  public class RemoveExperienceCommand : IRequest<ApiResult>
  {
    public string Id { get; set; } = default!;
  }
}
