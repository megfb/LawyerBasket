using LawyerBasket.AuthService.Application.Commands;
using LawyerBasket.AuthService.Application.Queries;
using LawyerBasket.Shared.Messaging.Events;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LawyerBasket.AuthService.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AuthController : ControllerBase
  {
    private readonly IPublishEndpoint _publish;
    private readonly IMediator _mediator;
    public AuthController(IMediator mediator, IPublishEndpoint publish)
    {
      _mediator = mediator;
      _publish = publish;
    }
    [HttpPost("Register")]
    public async Task<IActionResult> Register(RegisterCommand registerCommand)
    {
      return Ok(await _mediator.Send(registerCommand));
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginQuery loginQuery)
    {
      await _publish.Publish<TestEvent>(new TestEvent
      {
        Id = Guid.NewGuid().ToString(),
        Mesaj = "Auth Service Test Event",
        CreatedAt = DateTime.UtcNow
      }, context =>
      {
        context.SetRoutingKey("route.profileservice");
      });
      return Ok(await _mediator.Send(loginQuery));
    }

  }
}
