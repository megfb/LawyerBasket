using LawyerBasket.Gateway.Api.Dtos;
using LawyerBasket.Gateway.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace LawyerBasket.Gateway.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ProfileController : ControllerBase
  {
    private readonly IProfileService _profileService;

    public ProfileController(IProfileService profileService)
    {
      _profileService = profileService;
    }

    /// <summary>
    /// Gets current user profile with all details and their posts (API Composition Pattern)
    /// </summary>
    [HttpGet("GetUserProfileFull")]
    public async Task<IActionResult> GetUserProfileFull()
    {
      try
      {
        var result = await _profileService.GetUserProfileFullAsync();
        return Ok(new ApiResult<ProfileDto>
        {
          Data = result,
          IsSuccess = true,
          IsFail = false,
          Status = 200
        });
      }
      catch (Exception ex)
      {
        return Ok(new ApiResult<ProfileDto>
        {
          Data = null,
          ErrorMessage = new[] { ex.Message },
          IsSuccess = false,
          IsFail = true,
          Status = 500
        });
      }
    }
  }
}

