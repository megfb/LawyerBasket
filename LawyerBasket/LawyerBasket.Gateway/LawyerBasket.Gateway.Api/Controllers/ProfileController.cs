using LawyerBasket.Gateway.Api.Contracts;
using LawyerBasket.Gateway.Api.Dtos;
using LawyerBasket.Shared.Common.Response;
using Microsoft.AspNetCore.Mvc;

namespace LawyerBasket.Gateway.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ProfileController : ControllerBase
  {
    private readonly IProfileService _profileService;
    private readonly IUserProfileService _userProfileService;
    private readonly ISocialService _socialService;

    public ProfileController(
      IProfileService profileService, 
      IUserProfileService userProfileService,
      ISocialService socialService)
    {
      _profileService = profileService;
      _userProfileService = userProfileService;
      _socialService = socialService;
    }

    /// <summary>
    /// Gets current user profile with all details and their posts (API Composition Pattern)
    /// </summary>
    [HttpGet("GetUserProfileFull")]
    public async Task<IActionResult> GetUserProfileFull()
    {
      var result = await _profileService.GetUserProfileFullAsync();
      return Ok(result);
    }

    /// <summary>
    /// Gets user profiles by their IDs
    /// </summary>
    [HttpPost("GetUserProfilesByIds")]
    public async Task<IActionResult> GetUserProfilesByIds([FromBody] List<string> ids)
    {
      var result = await _userProfileService.GetUserProfilesByIdsAsync(ids);
      return Ok(result);
    }

    /// <summary>
    /// Deletes a friendship
    /// </summary>
    [HttpDelete("DeleteFriendship/{friendshipId}")]
    public async Task<IActionResult> DeleteFriendship(string friendshipId)
    {
      var result = await _socialService.DeleteFriendshipAsync(friendshipId);
      return Ok(result);
    }
  }
}

