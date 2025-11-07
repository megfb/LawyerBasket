using LawyerBasket.GatewayTest.Aggregators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LawyerBasket.GatewayTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserPostController : ControllerBase
    {
        private readonly UserProfileAggregator _userProfileAggregator;
        public UserPostController(UserProfileAggregator userProfileAggregator)
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
