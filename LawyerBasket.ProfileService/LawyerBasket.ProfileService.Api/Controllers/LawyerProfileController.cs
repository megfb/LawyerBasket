using LawyerBasket.ProfileService.Application.Commands;
using LawyerBasket.ProfileService.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LawyerBasket.ProfileService.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class LawyerProfileController : ControllerBase
  {
    private readonly IMediator _mediator;
    public LawyerProfileController(IMediator mediator)
    {
      _mediator = mediator;
    }

    [HttpPost("CreateLawyerProfile")]
    public async Task<IActionResult> CreateLawyerProfile(CreateLawyerProfileCommand command)
    {
      return Ok(await _mediator.Send(command));
    }
    [HttpPut("UpdateLawyerProfile/{id}")]
    public async Task<IActionResult> UpdateLawyerProfile(string id, UpdateLawyerProfileCommand command)
    {
      return Ok(await _mediator.Send(command));
    }

    [HttpGet("GetLawyerProfile/{id}")]
    public async Task<IActionResult> GetLawyerProfile(string id)
    {
      return Ok(await _mediator.Send(new GetLawyerProfileQuery { Id = id }));
    }
  }
}
