using LawyerBasket.AuthService.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LawyerBasket.AuthService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut("ChangePassword/{id}")]
        public async Task<IActionResult> ChangePassword(ChangePasswordCommand changePasswordCommand)
        {
            return Ok(await _mediator.Send(changePasswordCommand));
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveUser(RemoveUserCommand removeUserCommand)
        {
            return Ok(await _mediator.Send(removeUserCommand));
        }
    }
}
