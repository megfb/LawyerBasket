using LawyerBasket.ProfileService.Application.Dtos;
using LawyerBasket.Shared.Common.Response;
using MediatR;

namespace LawyerBasket.ProfileService.Application.Commands
{
    public class CreateLawyerExpertisementCommand : IRequest<ApiResult<List<LawyerExpertisementDto>>>
    {
        public string LawyerProfileId { get; set; } = default!;
        public List<string> ExpertisementIds { get; set; } = new();
    }
}
