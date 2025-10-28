using LawyerBasket.ProfileService.Application.Dtos;
using LawyerBasket.Shared.Common.Response;
using MediatR;

namespace LawyerBasket.ProfileService.Application.Queries
{
  public class GetExperiencesQuery : IRequest<ApiResult<List<ExperienceDto>>>
  {
    public string Id { get; set; }
  }
}
