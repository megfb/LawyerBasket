using LawyerBasket.PostService.Application.Commands;
using LawyerBasket.PostService.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LawyerBasket.PostService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CommentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("CreateComment")]
        public async Task<IActionResult> CreateComment(CreateCommentCommand createCommentCommand)
        {
            return Ok(await _mediator.Send(createCommentCommand));
        }

        [HttpDelete("RemoveComment/{id}")]
        public async Task<IActionResult> RemoveComment(RemoveCommentCommand removeCommentCommand)
        {
            return Ok(await _mediator.Send(removeCommentCommand));
        }

        [HttpGet("GetPostsCommentedByUser")]
        public async Task<IActionResult> GetPostsCommentedByUser()
        {
            return Ok(await _mediator.Send(new GetPostsCommentedByUserQuery()));
        }
    }
}
