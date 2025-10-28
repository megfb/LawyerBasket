using LawyerBasket.Shared.Common.Response;
using MediatR;

namespace LawyerBasket.ProfileService.Application.Commands
{
  public class CreateExpertisementToLawyerCommand : IRequest<ApiResult<string>>
  {
    public string LawyerProfileId { get; set; }
    public string ExpertisementId { get; set; }
  }
}


