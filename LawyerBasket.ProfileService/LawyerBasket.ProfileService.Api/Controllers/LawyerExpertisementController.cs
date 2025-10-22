using LawyerBasket.ProfileService.Application.Commands;
using LawyerBasket.ProfileService.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LawyerBasket.ProfileService.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class LawyerExpertisementController : ControllerBase
  {
    private readonly IMediator _mediator;
    public LawyerExpertisementController(IMediator mediator)
    {
      _mediator = mediator;
    }

    [HttpPost("CreateLawyerExpertisement")]
    public async Task<IActionResult> CreateLawyerExpertisement(CreateLawyerExpertisementCommand command)
    {
      return Ok(await _mediator.Send(command));
    }
    [HttpGet("GetLawyerExpertisements/{id}")]
    public async Task<IActionResult> GetLawyerExpertisements(string id)
    {
      return Ok(await _mediator.Send(new GetLawyerExpertisementsQuery { LawyerProfileId = id }));
    }
    [HttpDelete("RemoveLawyerExpertisement/{id}")]
    public async Task<IActionResult> RemoveLawyerExpertisement(string id)
    {
      return Ok(await _mediator.Send(new RemoveLawyerExpertisementCommand { Id = id }));
    }
  }
}
