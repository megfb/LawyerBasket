using LawyerBasket.ProfileService.Application.Commands;
using LawyerBasket.ProfileService.Application.Queries;
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
        [HttpPut("UpdateContact/{id}")]
        public async Task<IActionResult> UpdateContact(UpdateContactCommand updateContactCommand)
        {
            return Ok(await _mediator.Send(updateContactCommand));
        }
        [HttpDelete("RemoveContact/{id}")]
        public async Task<IActionResult> RemoveContact(string id)
        {
            return Ok(await _mediator.Send(new RemoveContactCommand { Id = id }));
        }
        [HttpGet("GetContact/{id}")]
        public async Task<IActionResult> GetContact(string id)
        {
            return Ok(await _mediator.Send(new GetContactQuery { Id = id }));
        }
    }
}
