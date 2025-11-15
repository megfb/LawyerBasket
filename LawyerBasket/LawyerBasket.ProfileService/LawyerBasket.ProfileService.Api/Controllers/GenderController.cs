using LawyerBasket.ProfileService.Application.Commands;
using LawyerBasket.ProfileService.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LawyerBasket.ProfileService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenderController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public GenderController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost("CreateGender")]
        public async Task<IActionResult> CreateGender(CreateGenderCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
        
        [HttpPut("UpdateGender/{id}")]
        public async Task<IActionResult> UpdateGender(UpdateGenderCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
        
        [HttpDelete("RemoveGender/{id}")]
        public async Task<IActionResult> RemoveGender(string id)
        {
            return Ok(await _mediator.Send(new RemoveGenderCommand { Id = id }));
        }
        
        [HttpGet("GetGender/{id}")]
        public async Task<IActionResult> GetGender(string id)
        {
            return Ok(await _mediator.Send(new GetGenderQuery { Id = id }));
        }
        
        [HttpGet("GetGenders")]
        public async Task<IActionResult> GetGenders()
        {
            return Ok(await _mediator.Send(new GetGendersQuery()));
        }
    }
}

