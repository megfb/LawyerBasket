using LawyerBasket.Shared.Common.Response;
using LawyerBasket.SocialService.Api.Application.Dtos;
using MediatR;

namespace LawyerBasket.SocialService.Api.Application.Queries
{
    public class GetUserFriendsQuery : IRequest<ApiResult<List<FriendshipDto>>>
    {
    }
}

