using LawyerBasket.ProfileService.Application.Dtos;
using LawyerBasket.Shared.Common.Response;
using MediatR;

namespace LawyerBasket.ProfileService.Application.Commands
{
    public class CreateAddressCommand : IRequest<ApiResult<AddressDto>>
    {
        public string UserProfileId { get; set; } = default!;
        public string AddressLine { get; set; } = default!;
        public string CityId { get; set; } = default!;
    }
}


