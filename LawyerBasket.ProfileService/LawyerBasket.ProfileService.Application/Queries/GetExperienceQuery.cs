using LawyerBasket.ProfileService.Application.Dtos;
using MediatR;

namespace LawyerBasket.ProfileService.Application.Queries
{
  public class GetExperienceQuery : IRequest<ApiResult<ExperienceDto>>
  {
    public string Id { get; set; }
  }
}
