using MediatR;

namespace LawyerBasket.ProfileService.Application.Commands
{
  public class CreateAcademyCommand : IRequest<ApiResult<string>>
  {
    public string LawyerProfileId { get; set; }
    public string University { get; set; }
    public string? Degree { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
  }
}


