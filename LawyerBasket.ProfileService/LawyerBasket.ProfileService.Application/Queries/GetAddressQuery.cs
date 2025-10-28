using LawyerBasket.ProfileService.Application.Dtos;
using LawyerBasket.Shared.Common.Response;
using MediatR;

namespace LawyerBasket.ProfileService.Application.Queries
{
  public class GetAddressQuery : IRequest<ApiResult<AddressDto>>
  {
    public string Id { get; set; }

  }
}
