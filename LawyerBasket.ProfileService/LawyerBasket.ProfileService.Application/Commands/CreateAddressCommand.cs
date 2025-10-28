using LawyerBasket.ProfileService.Application.Dtos;
using LawyerBasket.Shared.Common.Response;
using MediatR;

namespace LawyerBasket.ProfileService.Application.Commands
{
  public class CreateAddressCommand : IRequest<ApiResult<AddressDto>>
  {
    public string UserProfileId { get; set; }
    public string AddressLine { get; set; }
    public string CityId { get; set; }
  }
}


