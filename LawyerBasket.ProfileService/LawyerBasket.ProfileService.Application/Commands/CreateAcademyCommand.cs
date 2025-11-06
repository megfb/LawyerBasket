using LawyerBasket.ProfileService.Application.Dtos;
using LawyerBasket.Shared.Common.Response;
using MediatR;

namespace LawyerBasket.ProfileService.Application.Commands
{
  public class CreateAcademyCommand : IRequest<ApiResult<AcademyDto>>
  {
    public string LawyerProfileId { get; set; } = default!;
    public string University { get; set; } = default!;
    public string? Degree { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
  }
}


