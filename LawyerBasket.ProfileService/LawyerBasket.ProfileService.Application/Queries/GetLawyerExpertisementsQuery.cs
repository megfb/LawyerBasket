using LawyerBasket.ProfileService.Application.Dtos;
using LawyerBasket.Shared.Common.Response;
using MediatR;

namespace LawyerBasket.ProfileService.Application.Queries
{
    public class GetLawyerExpertisementsQuery : IRequest<ApiResult<List<LawyerExpertisementDto>>>
    {
        public string LawyerProfileId { get; set; } = default!;
    }
}
