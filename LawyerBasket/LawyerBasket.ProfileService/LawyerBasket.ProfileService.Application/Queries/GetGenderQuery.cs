using LawyerBasket.ProfileService.Application.Dtos;
using LawyerBasket.Shared.Common.Response;
using MediatR;

namespace LawyerBasket.ProfileService.Application.Queries
{
    public class GetGenderQuery : IRequest<ApiResult<GenderDto>>
    {
        public string Id { get; set; } = default!;
    }
}

