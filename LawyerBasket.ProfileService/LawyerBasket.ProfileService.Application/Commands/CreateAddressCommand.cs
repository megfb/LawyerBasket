using MediatR;

namespace LawyerBasket.ProfileService.Application.Commands
{
  public class CreateAddressCommand : IRequest<ApiResult<string>>
  {
    public string UserProfileId { get; set; }
    public string AddressLine { get; set; }
    public string CityId { get; set; }
  }
}


