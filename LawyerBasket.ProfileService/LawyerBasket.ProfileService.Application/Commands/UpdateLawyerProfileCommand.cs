using LawyerBasket.ProfileService.Application.Dtos;
using LawyerBasket.Shared.Common.Response;
using MediatR;

namespace LawyerBasket.ProfileService.Application.Commands
{
  public class UpdateLawyerProfileCommand : IRequest<ApiResult<LawyerProfileDto>>
  {
    public string Id { get; set; }
    public string UserProfileId { get; set; }
    public string BarAssociation { get; set; }
    public string BarNumber { get; set; }
    public string LicenseNumber { get; set; }
    public DateTime LicenseDate { get; set; }
  }
}
