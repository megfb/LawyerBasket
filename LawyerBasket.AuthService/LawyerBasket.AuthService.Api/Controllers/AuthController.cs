using LawyerBasket.AuthService.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LawyerBasket.AuthService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterCommand registerCommand)
        {
            return Ok(await _mediator.Send(registerCommand));
        }

    }
}
