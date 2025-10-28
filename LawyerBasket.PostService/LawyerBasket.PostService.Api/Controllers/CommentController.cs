using LawyerBasket.PostService.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
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
  }
}
