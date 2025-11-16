using LawyerBasket.Gateway.Api.Contracts;
using LawyerBasket.Gateway.Api.Dtos;
using LawyerBasket.Shared.Common.Response;
using System.Net;
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

        public async Task<ApiResult<UserProfileWDetailsDto>> GetUserProfileFullAsync()
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
                    return ApiResult<UserProfileWDetailsDto>.Fail("Failed to get user profile", (HttpStatusCode)response.StatusCode);
                }

                var content = await response.Content.ReadAsStringAsync();
                var apiResult = JsonSerializer.Deserialize<ApiResult<UserProfileWDetailsDto>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (apiResult == null)
                {
                    return ApiResult<UserProfileWDetailsDto>.Fail("Invalid response from service", HttpStatusCode.InternalServerError);
                }

                return apiResult.IsSuccess 
                    ? ApiResult<UserProfileWDetailsDto>.Success(apiResult.Data!, apiResult.Status)
                    : ApiResult<UserProfileWDetailsDto>.Fail(apiResult.ErrorMessage ?? new List<string> { "Unknown error" }, apiResult.Status);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching user profile full");
                return ApiResult<UserProfileWDetailsDto>.Fail(ex.Message, HttpStatusCode.InternalServerError);
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

        public async Task<ApiResult<List<UserProfileDto>>> GetUserProfilesByIdsAsync(List<string> ids)
        {
            try
            {
                if (ids == null || !ids.Any())
                {
                    _logger.LogWarning("Ids collection is null or empty");
                    return ApiResult<List<UserProfileDto>>.Fail("Ids collection cannot be null or empty", HttpStatusCode.BadRequest);
                }

                // Get token from current request
                var token = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].FirstOrDefault();
                var httpClient = CreateHttpClientWithToken(token);

                // Direct call to ProfileService GetUserProfilesByIds endpoint (bypassing gateway to avoid circular routing)
                var url = $"{_profileServiceUrl}/api/UserProfile/GetUserProfilesByIds";
                var requestBody = new { Ids = ids };
                var jsonContent = JsonSerializer.Serialize(requestBody);
                var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(url, content);
                
                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning("Failed to get user profiles by ids. Status: {StatusCode}", response.StatusCode);
                    return ApiResult<List<UserProfileDto>>.Fail("Failed to get user profiles", (HttpStatusCode)response.StatusCode);
                }

                var responseContent = await response.Content.ReadAsStringAsync();
                var apiResult = JsonSerializer.Deserialize<ApiResult<List<UserProfileDto>>>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (apiResult == null)
                {
                    return ApiResult<List<UserProfileDto>>.Fail("Invalid response from service", HttpStatusCode.InternalServerError);
                }

                return apiResult.IsSuccess 
                    ? ApiResult<List<UserProfileDto>>.Success(apiResult.Data ?? new List<UserProfileDto>(), apiResult.Status)
                    : ApiResult<List<UserProfileDto>>.Fail(apiResult.ErrorMessage ?? new List<string> { "Unknown error" }, apiResult.Status);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching user profiles by ids");
                return ApiResult<List<UserProfileDto>>.Fail(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}

