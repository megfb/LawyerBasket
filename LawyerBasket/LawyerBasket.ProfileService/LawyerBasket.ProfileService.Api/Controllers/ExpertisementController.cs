using LawyerBasket.ProfileService.Application.Commands;
using LawyerBasket.ProfileService.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LawyerBasket.ProfileService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpertisementController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public ExpertisementController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost("CreateExpertisement")]
        public async Task<IActionResult> CreateExpertisement(CreateExpertisementCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
        
        [HttpPut("UpdateExpertisement/{id}")]
        public async Task<IActionResult> UpdateExpertisement(UpdateExpertisementCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
        
        [HttpDelete("RemoveExpertisement/{id}")]
        public async Task<IActionResult> RemoveExpertisement(string id)
        {
            return Ok(await _mediator.Send(new RemoveExpertisementCommand { Id = id }));
        }
        
        [HttpGet("GetExpertisement/{id}")]
        public async Task<IActionResult> GetExpertisement(string id)
        {
            return Ok(await _mediator.Send(new GetExpertisementQuery { Id = id }));
        }
        
        [HttpGet("GetExpertisements")]
        public async Task<IActionResult> GetExpertisements()
        {
            return Ok(await _mediator.Send(new GetExpertisementsQuery()));
        }
    }
}

