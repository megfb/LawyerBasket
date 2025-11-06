using LawyerBasket.ProfileService.Application.Dtos;
using LawyerBasket.Shared.Common.Response;
using MediatR;

namespace LawyerBasket.ProfileService.Application.Queries
{
    public class GetAcademyQuery : IRequest<ApiResult<AcademyDto>>
    {
        public string Id { get; set; } = default!;
    }
}
