using LawyerBasket.GatewayTest.Contracts;
using LawyerBasket.GatewayTest.Dtos.ProfileService;
using LawyerBasket.Shared.Common.Response;

namespace LawyerBasket.GatewayTest.Aggregators
{
  public class UserProfileAggregator : IAggregator<UserProfileDto>
  {
    private readonly HttpClient _httpClient;
    private readonly string _userProfileUrl;
    public UserProfileAggregator(HttpClient httpClient, IConfiguration configuration)
    {
      _httpClient = httpClient;
      _userProfileUrl = configuration[$"ProfileServiceUrls:UserProfileUrl"] ?? throw new ArgumentNullException(nameof(_userProfileUrl));
    }
    public async Task<UserProfileDto> AggregateAsync(string id)
    {
      var url = $"{_userProfileUrl}/GetUserProfile/{id}";
      var response = await _httpClient.GetFromJsonAsync<ApiResult<UserProfileDto>>(url);
      return response?.Data!;
    }
  }
}
