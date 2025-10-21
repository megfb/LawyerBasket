using LawyerBasket.ProfileService.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LawyerBasket.ProfileService.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ContactController : ControllerBase
  {

    private IMediator _mediator;
    public ContactController(IMediator mediator)
    {
      _mediator = mediator;
    }

    [HttpPost("CreateContact")]
    public async Task<IActionResult> CreateContact(CreateContactCommand createContactCommand)
    {
      return Ok(await _mediator.Send(createContactCommand));
    }
  }
}
