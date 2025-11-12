using LawyerBasket.PostService.Application.Commands;
using LawyerBasket.PostService.Application.Contracts.Api;
using LawyerBasket.PostService.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LawyerBasket.PostService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICurrentUserService _currentUserService;
        public PostController(IMediator mediator, ICurrentUserService currentUserService)
        {
            _mediator = mediator;
            _currentUserService = currentUserService;
        }

        [HttpPost("CreatePost")]
        public async Task<IActionResult> CreatePost(CreatePostCommand createPostCommand)
        {
            return Ok(await _mediator.Send(createPostCommand));
        }
        [HttpGet("GetPost/{id}")]
        public async Task<IActionResult> GetPost(string id)
        {
            return Ok(await _mediator.Send(new GetPostQuery { Id = id }));
        }
        [HttpGet("GetPosts")]
        public async Task<IActionResult> GetPosts()
        {
            return Ok(await _mediator.Send(new GetPostsQuery ()));
        }
        [HttpDelete("RemovePost/{id}")]
        public async Task<IActionResult> RemovePost(string id)
        {
            return Ok(await _mediator.Send(new RemovePostCommand { Id = id }));
        }
    }
}
