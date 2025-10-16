using LawyerBasket.AuthService.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LawyerBasket.AuthService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RoleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("CreateRole")]
        public async Task<IActionResult> CreateRole(CreateRoleCommand createRoleCommand)
        {
            return Ok(await _mediator.Send(createRoleCommand));
        }
        [HttpDelete("RemoveRole/{id}")]
        public async Task<IActionResult> RemoveRole(string id)
        {
            return Ok(await _mediator.Send(new RemoveRoleCommand { Id = id }));
        }

        [HttpPut("UpdateRole")]
        public async Task<IActionResult> UpdateRole(UpdateRoleCommand updateRoleCommand)
        {
            return Ok(await _mediator.Send(updateRoleCommand));
        }

        [HttpGet("GetRoles")]
        public async Task<IActionResult> GetRoles()
        {
            return Ok(await _mediator.Send(new Application.Queries.GetRolesQuery()));
        }
    }
}
