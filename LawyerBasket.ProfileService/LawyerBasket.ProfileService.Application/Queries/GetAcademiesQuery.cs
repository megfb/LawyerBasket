using LawyerBasket.ProfileService.Application.Dtos;
using LawyerBasket.Shared.Common.Response;
using MediatR;

namespace LawyerBasket.ProfileService.Application.Queries
{
    public class GetAcademiesQuery : IRequest<ApiResult<List<AcademyDto>>>
    {
        public string Id { get; set; } = default!;
    }
}
