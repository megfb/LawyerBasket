using LawyerBasket.PostService.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LawyerBasket.PostService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public LikesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("CreateLike")]
        public async Task<IActionResult> CreateLike(CreateLikesCommand createLikesCommand)
        {
            return Ok(await _mediator.Send(createLikesCommand));
        }
        [HttpDelete("RemoveLike/{id}")]
        public async Task<IActionResult> RemoveLike(RemoveLikesCommand removeLikesCommand)
        {
            return Ok(await _mediator.Send(removeLikesCommand));
        }
    }
}
