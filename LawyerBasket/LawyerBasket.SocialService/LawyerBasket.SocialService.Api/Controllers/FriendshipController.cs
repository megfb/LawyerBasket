using LawyerBasket.SocialService.Api.Application.Commands;
using LawyerBasket.SocialService.Api.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LawyerBasket.SocialService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FriendshipController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FriendshipController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateFriendship(CreateFriendshipCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpGet("GetFriends")]
        public async Task<IActionResult> GetFriends()
        {
            return Ok(await _mediator.Send(new GetUserFriendsQuery()));
        }

        [HttpDelete("{friendshipId}")]
        public async Task<IActionResult> DeleteFriendship(string friendshipId)
        {
            var command = new DeleteFriendshipCommand { FriendshipId = friendshipId };
            return Ok(await _mediator.Send(command));
        }
    }
}
