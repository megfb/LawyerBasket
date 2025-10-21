using LawyerBasket.AuthService.Application.Contracts.Api;
using System.Security.Claims;

namespace LawyerBasket.AuthService.Api
{
  public class CurrentUserService : ICurrentUserService
  {
    private readonly IHttpContextAccessor _httpContextAccessor;
    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
      _httpContextAccessor = httpContextAccessor;
    }
    public string? UserId => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
  }
}
