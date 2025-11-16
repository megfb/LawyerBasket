using LawyerBasket.ProfileService.Application.Dtos;
using LawyerBasket.Shared.Common.Response;
using MediatR;

namespace LawyerBasket.ProfileService.Application.Queries
{
    public class GetUserProfilesByIdsQuery : IRequest<ApiResult<IEnumerable<UserProfileDto>>>
    {
        public IEnumerable<string> Ids { get; set; } = default!;
    }
}

