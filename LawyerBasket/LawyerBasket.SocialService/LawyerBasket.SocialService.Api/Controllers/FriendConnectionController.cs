using LawyerBasket.SocialService.Api.Application.Commands;
using LawyerBasket.SocialService.Api.Domain.Contracts.Application;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LawyerBasket.SocialService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendConnectionController : ControllerBase
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IMediator _mediator;
        public FriendConnectionController(IMediator mediator, ICurrentUserService currentUserService)
        {
            _mediator = mediator;
            _currentUserService = currentUserService;
        }
        [HttpPost("CreateFriendConnection")]
        public async Task<IActionResult> CreateFriendConnection(CreateFriendConnectionCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
