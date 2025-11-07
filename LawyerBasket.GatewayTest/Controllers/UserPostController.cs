using LawyerBasket.GatewayTest.Aggregators;
using LawyerBasket.GatewayTest.Composes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LawyerBasket.GatewayTest.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [AllowAnonymous]
  public class UserPostController : ControllerBase
  {
    private readonly UserProfilePostCompose _userProfileAggregator;
    public UserPostController(UserProfilePostCompose userProfileAggregator)
    {
      _userProfileAggregator = userProfileAggregator;
    }

    [HttpGet("GetUserProfile/{id}")]
    public async Task<IActionResult> GetUserProfile(string id)
    {
      var result = await _userProfileAggregator.AggregateAsync(id);
      return Ok(result);
    }
  }
}
