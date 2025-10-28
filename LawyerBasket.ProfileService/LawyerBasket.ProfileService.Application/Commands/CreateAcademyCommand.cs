using LawyerBasket.ProfileService.Application.Dtos;
using LawyerBasket.Shared.Common.Response;
using MediatR;

namespace LawyerBasket.ProfileService.Application.Commands
{
  public class CreateAcademyCommand : IRequest<ApiResult<AcademyDto>>
  {
    public string LawyerProfileId { get; set; }
    public string University { get; set; }
    public string? Degree { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
  }
}


