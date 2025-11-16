using LawyerBasket.Gateway.Api.Contracts;
using LawyerBasket.Gateway.Api.Dtos;
using LawyerBasket.Shared.Common.Response;
using System.Net;
using System.Text.Json;

namespace LawyerBasket.Gateway.Api.Services
{
    public class SocialService : ISocialService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IUserProfileService _userProfileService;
        private readonly ILogger<SocialService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _socialServiceUrl;

        public SocialService(
            IHttpClientFactory httpClientFactory,
            IUserProfileService userProfileService,
            ILogger<SocialService> logger,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _userProfileService = userProfileService;
            _logger = logger;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _socialServiceUrl = _configuration["ServiceUrls:SocialService"]
                ?? throw new InvalidOperationException("ServiceUrls:SocialService configuration is missing.");
        }

        public async Task<ApiResult<List<FriendshipDto>>> GetUserFriendsAsync()
        {
            try
            {
                // Get token from current request
                var token = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].FirstOrDefault();
                var httpClient = CreateHttpClientWithToken(token);

                // Direct call to SocialService GetFriends endpoint (bypassing gateway to avoid circular routing)
                var url = $"{_socialServiceUrl}/api/Friendship/GetFriends";
                _logger.LogInformation("Calling SocialService to get user friends, URL: {Url}", url);

                var response = await httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogWarning("Failed to get user friends. Status: {StatusCode}, Response: {Response}, URL: {Url}",
                        response.StatusCode, errorContent, url);
                    return ApiResult<List<FriendshipDto>>.Fail("Failed to get user friends", (HttpStatusCode)response.StatusCode);
                }

                var content = await response.Content.ReadAsStringAsync();
                var apiResult = JsonSerializer.Deserialize<ApiResult<List<FriendshipDto>>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (apiResult == null)
                {
                    _logger.LogWarning("Failed to deserialize response from SocialService");
                    return ApiResult<List<FriendshipDto>>.Fail("Failed to deserialize response", HttpStatusCode.InternalServerError);
                }

                return apiResult;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching user friends from SocialService");
                return ApiResult<List<FriendshipDto>>.Fail(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ApiResult<List<FriendWithProfileDto>>> GetUserFriendsWithProfilesAsync()
        {
            try
            {
                // Get friends from SocialService
                var friendsResult = await GetUserFriendsAsync();

                if (!friendsResult.IsSuccess || friendsResult.Data == null || !friendsResult.Data.Any())
                {
                    return ApiResult<List<FriendWithProfileDto>>.Success(new List<FriendWithProfileDto>());
                }

                // Get friend user IDs
                var friendUserIds = friendsResult.Data.Select(f => f.FriendUserId).Distinct().ToList();

                // Get profiles for all friends
                var profilesResult = await _userProfileService.GetUserProfilesByIdsAsync(friendUserIds);

                if (!profilesResult.IsSuccess || profilesResult.Data == null)
                {
                    _logger.LogWarning("Failed to get friend profiles, but returning friends without profiles");
                    // Return friends without profiles if profile fetch fails
                    var friendsWithoutProfiles = friendsResult.Data.Select(f => new FriendWithProfileDto
                    {
                        FriendshipId = f.Id,
                        FriendUserId = f.FriendUserId,
                        FriendshipCreatedAt = f.CreatedAt,
                        Profile = null
                    }).ToList();
                    return ApiResult<List<FriendWithProfileDto>>.Success(friendsWithoutProfiles);
                }

                // Create a dictionary for quick lookup
                var profilesDict = profilesResult.Data.ToDictionary(p => p.Id, p => p);

                // Combine friends with profiles
                var friendsWithProfiles = friendsResult.Data.Select(f => new FriendWithProfileDto
                {
                    FriendshipId = f.Id,
                    FriendUserId = f.FriendUserId,
                    FriendshipCreatedAt = f.CreatedAt,
                    Profile = profilesDict.ContainsKey(f.FriendUserId) ? profilesDict[f.FriendUserId] : null
                }).ToList();

                return ApiResult<List<FriendWithProfileDto>>.Success(friendsWithProfiles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting friends with profiles");
                return ApiResult<List<FriendWithProfileDto>>.Fail(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ApiResult> DeleteFriendshipAsync(string friendshipId)
        {
            try
            {
                // Get token from current request
                var token = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].FirstOrDefault();
                var httpClient = CreateHttpClientWithToken(token);

                // Direct call to SocialService DeleteFriendship endpoint
                var url = $"{_socialServiceUrl}/api/Friendship/{friendshipId}";
                _logger.LogInformation("Calling SocialService to delete friendship, URL: {Url}", url);

                var response = await httpClient.DeleteAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogWarning("Failed to delete friendship. Status: {StatusCode}, Response: {Response}, URL: {Url}",
                        response.StatusCode, errorContent, url);
                    return ApiResult.Fail("Failed to delete friendship", (HttpStatusCode)response.StatusCode);
                }

                var content = await response.Content.ReadAsStringAsync();
                var apiResult = JsonSerializer.Deserialize<ApiResult>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (apiResult == null)
                {
                    _logger.LogWarning("Failed to deserialize response from SocialService");
                    return ApiResult.Fail("Failed to deserialize response", HttpStatusCode.InternalServerError);
                }

                return apiResult;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting friendship from SocialService");
                return ApiResult.Fail(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        private HttpClient CreateHttpClientWithToken(string? token)
        {
            var httpClient = _httpClientFactory.CreateClient();
            if (!string.IsNullOrEmpty(token))
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.Replace("Bearer ", ""));
            }
            return httpClient;
        }
    }
}

