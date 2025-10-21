using LawyerBasket.ProfileService.Application.Dtos;
using MediatR;

namespace LawyerBasket.ProfileService.Application.Commands
{
  public class UpdateAddressCommand : IRequest<ApiResult<AddressDto>>
  {
    public string Id { get; set; }
    public string AddressLine { get; set; }
    public string CityId { get; set; }
  }
}
