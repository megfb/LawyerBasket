using LawyerBasket.PostService.Application.Commands;
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
    public PostController(IMediator mediator)
    {
      _mediator = mediator;
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
    [HttpGet("GetPosts/{id}")]
    public async Task<IActionResult> GetPosts(string id)
    {
      return Ok(await _mediator.Send(new GetPostsQuery { Id = id }));
    }
  }
}
