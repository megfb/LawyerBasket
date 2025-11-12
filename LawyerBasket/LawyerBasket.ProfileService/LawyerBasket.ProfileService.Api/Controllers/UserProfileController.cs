using LawyerBasket.ProfileService.Application.Commands;
using LawyerBasket.ProfileService.Application.Queries;
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

    [HttpPost("CreateUserProfile")]
    public async Task<IActionResult> CreateUserProfile(CreateUserProfileCommand command)
    {
      return Ok(await _mediator.Send(command));
    }

    [HttpPut("UpdateUserProfile")]
    public async Task<IActionResult> UpdateUserProfile(UpdateUserProfileCommand command)
    {
      return Ok(await _mediator.Send(command));
    }

    [HttpGet("GetUserProfile/{id}")]
    public async Task<IActionResult> GetUserProfile(string id)
    {
      return Ok(await _mediator.Send(new GetUserProfileQuery { Id = id }));
    }
    [HttpGet("GetUserProfileFull")]
    public async Task<IActionResult> GetUserProfileFull()
    {
      return Ok(await _mediator.Send(new GetUserProfileWDetailsQuery()));
    }
    //[HttpPost("CreateUserProfileWithDetails")] // orchestrator
    //public async Task<IActionResult> CreateUserProfileWithDetails(CreateUserProfileOrchestratorCommand command)
    //{
    //  return Ok(await _mediator.Send(command));
    //}

  }
}


