using LawyerBasket.ProfileService.Application.Commands;
using LawyerBasket.ProfileService.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LawyerBasket.ProfileService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public CityController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost("CreateCity")]
        public async Task<IActionResult> CreateCity(CreateCityCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
        
        [HttpPut("UpdateCity/{id}")]
        public async Task<IActionResult> UpdateCity(UpdateCityCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
        
        [HttpDelete("RemoveCity/{id}")]
        public async Task<IActionResult> RemoveCity(string id)
        {
            return Ok(await _mediator.Send(new RemoveCityCommand { Id = id }));
        }
        
        [HttpGet("GetCity/{id}")]
        public async Task<IActionResult> GetCity(string id)
        {
            return Ok(await _mediator.Send(new GetCityQuery { Id = id }));
        }
        
        [HttpGet("GetCities")]
        public async Task<IActionResult> GetCities()
        {
            return Ok(await _mediator.Send(new GetCitiesQuery()));
        }
    }
}

