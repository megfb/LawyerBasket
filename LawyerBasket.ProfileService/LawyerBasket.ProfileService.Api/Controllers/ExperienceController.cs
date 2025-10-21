using LawyerBasket.ProfileService.Application.Commands;
using LawyerBasket.ProfileService.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LawyerBasket.ProfileService.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ExperienceController : ControllerBase
  {
    private readonly IMediator _mediator;
    public ExperienceController(IMediator mediator)
    {
      _mediator = mediator;
    }
    [HttpPost("CreateExperience")]
    public async Task<IActionResult> CreateExperience(CreateExperienceCommand command)
    {
      return Ok(await _mediator.Send(command));
    }
    [HttpPut("UpdateExperience/{id}")]
    public async Task<IActionResult> UpdateExperience(UpdateExperienceCommand command)
    {
      return Ok(await _mediator.Send(command));
    }
    [HttpDelete("RemoveExperience/{id}")]
    public async Task<IActionResult> RemoveExperience(string id)
    {
      return Ok(await _mediator.Send(new RemoveExperienceCommand { Id = id }));
    }
    [HttpGet("GetExperience/{id}")]
    public async Task<IActionResult> GetExperience(string id)
    {
      return Ok(await _mediator.Send(new GetExperienceQuery { Id = id }));
    }
    [HttpGet("GetExperiences")]
    public async Task<IActionResult> GetExperiences()
    {
      return Ok(await _mediator.Send(new GetExperiencesQuery()));
    }
  }
}
