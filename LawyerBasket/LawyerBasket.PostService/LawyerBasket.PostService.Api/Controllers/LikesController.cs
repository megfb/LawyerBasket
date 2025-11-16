using LawyerBasket.PostService.Application.Commands;
using LawyerBasket.PostService.Application.Queries;
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

        [HttpGet("GetPostsLikedByUser")]
        public async Task<IActionResult> GetPostsLikedByUser()
        {
            return Ok(await _mediator.Send(new GetPostsLikedByUserQuery()));
        }

        [HttpGet("GetPostLikes/{postId}")]
        public async Task<IActionResult> GetPostLikes(string postId)
        {
            return Ok(await _mediator.Send(new GetPostLikesQuery { PostId = postId }));
        }
    }
}
