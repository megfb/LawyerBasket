using LawyerBasket.ProfileService.Application.Dtos;
using LawyerBasket.Shared.Common.Response;
using MediatR;

namespace LawyerBasket.ProfileService.Application.Commands
{
  public class CreateLawyerExpertisementCommand : IRequest<ApiResult<LawyerExpertisementDto>>
  {
    public string LawyerProfileId { get; set; }
    public string ExpertisementId { get; set; }
  }
}
