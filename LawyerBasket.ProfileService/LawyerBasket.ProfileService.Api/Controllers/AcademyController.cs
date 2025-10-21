using LawyerBasket.ProfileService.Application.Commands;
using LawyerBasket.ProfileService.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LawyerBasket.ProfileService.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AcademyController : ControllerBase
  {
    private readonly IMediator _mediator;
    public AcademyController(IMediator mediator)
    {
      _mediator = mediator;
    }
    [HttpPost]
    public async Task<IActionResult> CreateAcademy(CreateAcademyCommand command)
    {
      return Ok(await _mediator.Send(command));
    }
    [HttpPut("UpdateAcademy/{id}")]
    public async Task<IActionResult> UpdateAcademy(UpdateAcademyCommand command)
    {
      return Ok(await _mediator.Send(command));
    }
    [HttpDelete("RemoveAcademy/{id}")]
    public async Task<IActionResult> RemoveAcademy(string id)
    {
      return Ok(await _mediator.Send(new RemoveAcademyCommand { Id = id }));
    }
    [HttpGet("GetAcademy/{id}")]
    public async Task<IActionResult> GetAcademyById(string id)
    {
      return Ok(await _mediator.Send(new GetAcademyQuery { Id = id }));
    }
    [HttpGet("GetAcademies")]
    public async Task<IActionResult> GetAcademies()
    {
      return Ok(await _mediator.Send(new GetAcademiesQuery()));
    }
  }
}
