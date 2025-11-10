using LawyerBasket.ProfileService.Application.Commands;
using LawyerBasket.ProfileService.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LawyerBasket.ProfileService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IMediator _mediatr;

        public AddressController(IMediator mediator)
        {
            _mediatr = mediator;
        }

        [HttpPost("CreateAddress")]
        public async Task<IActionResult> CreateAddress(CreateAddressCommand createAddressCommand)
        {
            return Ok(await _mediatr.Send(createAddressCommand));
        }
        [HttpPut("UpdateAddress/{id}")]
        public async Task<IActionResult> UpdateAddress(UpdateAddressCommand updateAddressCommand)
        {
            return Ok(await _mediatr.Send(updateAddressCommand));
        }
        [HttpDelete("RemoveAddress/{id}")]
        public async Task<IActionResult> DeleteAddress(string id)
        {
            return Ok(await _mediatr.Send(new RemoveAddressCommand { Id = id }));
        }

        [HttpGet("GetAddress")]
        public async Task<IActionResult> GetAddress()
        {
            return Ok(await _mediatr.Send(new GetAddressQuery()));
        }
    }
}
