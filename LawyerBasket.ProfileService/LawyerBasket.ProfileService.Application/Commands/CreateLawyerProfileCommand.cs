using MediatR;

namespace LawyerBasket.ProfileService.Application.Commands
{
  public class CreateLawyerProfileCommand : IRequest<ApiResult<string>>
  {
    public string UserProfileId { get; set; }
    public string BarAssociation { get; set; }
    public string BarNumber { get; set; }
    public string LicenseNumber { get; set; }
    public DateTime LicenseDate { get; set; }
  }
}


