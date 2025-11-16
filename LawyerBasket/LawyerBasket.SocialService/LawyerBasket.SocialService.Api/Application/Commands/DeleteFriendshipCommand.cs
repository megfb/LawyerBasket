using LawyerBasket.Shared.Common.Response;
using MediatR;

namespace LawyerBasket.SocialService.Api.Application.Commands
{
    public class DeleteFriendshipCommand : IRequest<ApiResult>
    {
        public string FriendshipId { get; set; } = default!;
    }
}

