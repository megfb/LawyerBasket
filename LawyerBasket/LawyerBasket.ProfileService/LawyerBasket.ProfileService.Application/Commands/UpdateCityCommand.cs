using LawyerBasket.ProfileService.Application.Dtos;
using LawyerBasket.Shared.Common.Response;
using MediatR;

namespace LawyerBasket.ProfileService.Application.Commands
{
    public class UpdateCityCommand : IRequest<ApiResult<CityDto>>
    {
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public DateTime UpdatedAt { get; set; }
    }
}

