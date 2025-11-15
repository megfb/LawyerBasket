using LawyerBasket.ProfileService.Application.Dtos;
using LawyerBasket.Shared.Common.Response;
using MediatR;

namespace LawyerBasket.ProfileService.Application.Commands
{
    public class CreateCityCommand : IRequest<ApiResult<CityDto>>
    {
        public string Name { get; set; } = default!;
    }
}

