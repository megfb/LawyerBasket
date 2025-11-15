using LawyerBasket.ProfileService.Application.Dtos;
using LawyerBasket.Shared.Common.Response;
using MediatR;

namespace LawyerBasket.ProfileService.Application.Queries
{
    public class GetExpertisementsQuery : IRequest<ApiResult<List<ExpertisementDto>>>
    {
    }
}

