using LawyerBasket.Gateway.Api.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace LawyerBasket.Gateway.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class LikesController : ControllerBase
  {
    private readonly ILikesService _likesService;

    public LikesController(ILikesService likesService)
    {
      _likesService = likesService;
    }

    /// <summary>
    /// Gets users who liked a specific post
    /// </summary>
    [HttpGet("GetPostLikesWithUsers/{postId}")]
    public async Task<IActionResult> GetPostLikesWithUsers(string postId)
    {
      var result = await _likesService.GetPostLikesWithUsersAsync(postId);
      return Ok(result);
    }
  }
}

