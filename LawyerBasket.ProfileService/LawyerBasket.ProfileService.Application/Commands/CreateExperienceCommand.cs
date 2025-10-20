using MediatR;

namespace LawyerBasket.ProfileService.Application.Commands
{
  public class CreateExperienceCommand : IRequest<ApiResult<string>>
  {
    public string LawyerProfileId { get; set; }
    public string CompanyName { get; set; }
    public string Position { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Description { get; set; }
  }
}


