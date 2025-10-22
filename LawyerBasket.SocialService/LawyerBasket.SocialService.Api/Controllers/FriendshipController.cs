using LawyerBasket.SocialService.Api.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LawyerBasket.SocialService.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
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
  }
}
