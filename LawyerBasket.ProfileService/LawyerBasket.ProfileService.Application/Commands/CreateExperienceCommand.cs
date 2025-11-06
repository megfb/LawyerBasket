using LawyerBasket.ProfileService.Application.Dtos;
using LawyerBasket.Shared.Common.Response;
using MediatR;

namespace LawyerBasket.ProfileService.Application.Commands
{
  public class CreateExperienceCommand : IRequest<ApiResult<ExperienceDto>>
  {
    public string LawyerProfileId { get; set; } = default!;
    public string CompanyName { get; set; } = default!;
    public string Position { get; set; } = default!;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Description { get; set; } = default!;
  } 
}


