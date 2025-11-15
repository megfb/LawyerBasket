using LawyerBasket.Gateway.Api.Dtos;
using System.Text.Json;

namespace LawyerBasket.Gateway.Api.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<UserProfileService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _profileServiceUrl;

        public UserProfileService(
            IHttpClientFactory httpClientFactory,
            ILogger<UserProfileService> logger,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _profileServiceUrl = _configuration["ServiceUrls:ProfileService"] 
                ?? throw new InvalidOperationException("ServiceUrls:ProfileService configuration is missing.");
        }

        public async Task<UserProfileWDetailsDto?> GetUserProfileFullAsync()
        {
            try
            {
                // Get token from current request
                var token = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].FirstOrDefault();
                var httpClient = CreateHttpClientWithToken(token);

                // Direct call to ProfileService GetUserProfileFull endpoint (bypassing gateway to avoid circular routing)
                var url = $"{_profileServiceUrl}/api/UserProfile/GetUserProfileFull";
                var response = await httpClient.GetAsync(url);
                
                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning("Failed to get user profile full. Status: {StatusCode}", response.StatusCode);
                    return null;
                }

                var content = await response.Content.ReadAsStringAsync();
                var apiResult = JsonSerializer.Deserialize<ApiResult<UserProfileWDetailsDto>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return apiResult?.IsSuccess == true ? apiResult.Data : null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching user profile full");
                return null;
            }
        }

        private HttpClient CreateHttpClientWithToken(string? token)
        {
            var httpClient = _httpClientFactory.CreateClient();
            
            if (!string.IsNullOrEmpty(token))
            {
                // Remove "Bearer " prefix if exists
                var tokenValue = token.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase) 
                    ? token.Substring(7) 
                    : token;
                    
                httpClient.DefaultRequestHeaders.Authorization = 
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenValue);
            }
            
            return httpClient;
        }
    }
}

