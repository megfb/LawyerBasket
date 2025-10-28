using LawyerBasket.SocialService.Api.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LawyerBasket.SocialService.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class FriendConnectionController : ControllerBase
  {
    private readonly IMediator _mediator;
    public FriendConnectionController(IMediator mediator)
    {
      _mediator = mediator;
    }
    [HttpPost("CreateFriendConnection")]
    public async Task<IActionResult> CreateFriendConnection(CreateFriendConnectionCommand command)
    {
      return Ok(await _mediator.Send(command));
    }
  }
}
