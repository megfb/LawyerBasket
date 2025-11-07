using LawyerBasket.GatewayTest.Contracts;
using LawyerBasket.GatewayTest.Dtos.ProfileService;

namespace LawyerBasket.GatewayTest.Aggregators
{
    public class UserProfileAggregator : IAggregator<UserProfileDto>
    {
        private readonly HttpClient _httpClient;
        private readonly string _userProfileUrl;
        public UserProfileAggregator(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _userProfileUrl = configuration[$"ProfileServiceUrl:UserProfileUrl"]!;
        }
        public async Task<UserProfileDto> AggregateAsync(string id)
        {
            var url = $"{_userProfileUrl}/GetUserProfile/{id}";
            return await _httpClient.GetFromJsonAsync<UserProfileDto>(url);
        }
    }
}
