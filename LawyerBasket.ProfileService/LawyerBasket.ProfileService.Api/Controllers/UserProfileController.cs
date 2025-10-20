using LawyerBasket.ProfileService.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LawyerBasket.ProfileService.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class UserProfileController : ControllerBase
  {
    private readonly IMediator _mediator;
    public UserProfileController(IMediator mediator)
    {
      _mediator = mediator;
    }

    [HttpPost("CreateUserProfile")] // sadece user
    public async Task<IActionResult> CreateUserProfile(CreateUserProfileCommand command)
    {
      return Ok(await _mediator.Send(command));
    }

    [HttpPost("CreateUserProfileWithDetails")] // orchestrator
    public async Task<IActionResult> CreateUserProfileWithDetails(CreateUserProfileOrchestratorCommand command)
    {
      return Ok(await _mediator.Send(command));
    }
  }
}


