using LawyerBasket.Gateway.Api.Contracts;
using LawyerBasket.Gateway.Api.Dtos;
using LawyerBasket.Shared.Common.Response;
using System.Net;
using System.Text.Json;

namespace LawyerBasket.Gateway.Api.Services
{
    public class LikesService : ILikesService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<LikesService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserProfileService _userProfileService;
        private readonly string _postServiceUrl;

        public LikesService(
            IHttpClientFactory httpClientFactory,
            ILogger<LikesService> logger,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor,
            IUserProfileService userProfileService)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _userProfileService = userProfileService;
            _postServiceUrl = _configuration["ServiceUrls:PostService"]
                ?? throw new InvalidOperationException("ServiceUrls:PostService configuration is missing.");
        }

        public async Task<ApiResult<List<PostDto>>> GetPostsLikedByUserAsync()
        {
            try
            {
                var token = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].FirstOrDefault();
                var httpClient = CreateHttpClientWithToken(token);

                var url = $"{_postServiceUrl}/api/Likes/GetPostsLikedByUser";
                var response = await httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning("Failed to get posts liked by user. Status: {StatusCode}", response.StatusCode);
                    return ApiResult<List<PostDto>>.Fail("Failed to get liked posts", (HttpStatusCode)response.StatusCode);
                }

                var content = await response.Content.ReadAsStringAsync();
                var apiResult = JsonSerializer.Deserialize<ApiResult<List<PostDto>>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (apiResult == null)
                {
                    return ApiResult<List<PostDto>>.Fail("Invalid response from service", HttpStatusCode.InternalServerError);
                }

                return apiResult.IsSuccess 
                    ? ApiResult<List<PostDto>>.Success(apiResult.Data ?? new List<PostDto>(), apiResult.Status)
                    : ApiResult<List<PostDto>>.Fail(apiResult.ErrorMessage ?? new List<string> { "Unknown error" }, apiResult.Status);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching posts liked by user");
                return ApiResult<List<PostDto>>.Fail(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        private HttpClient CreateHttpClientWithToken(string? token)
        {
            var httpClient = _httpClientFactory.CreateClient();

            if (!string.IsNullOrEmpty(token))
            {
                var tokenValue = token.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase)
                    ? token.Substring(7)
                    : token;

                httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenValue);
            }

            return httpClient;
        }

        public async Task<ApiResult<List<PostLikeUserDto>>> GetPostLikesWithUsersAsync(string postId)
        {
            try
            {
                if (string.IsNullOrEmpty(postId))
                {
                    return ApiResult<List<PostLikeUserDto>>.Fail("Post ID is required", HttpStatusCode.BadRequest);
                }

                var token = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].FirstOrDefault();
                var httpClient = CreateHttpClientWithToken(token);

                // Get likes for the post - direct call to PostService (bypassing gateway to avoid circular routing)
                var likesUrl = $"{_postServiceUrl}/api/Likes/GetPostLikes/{postId}";
                _logger.LogInformation("Calling PostService to get likes for PostId: {PostId}, URL: {Url}", postId, likesUrl);
                
                var likesResponse = await httpClient.GetAsync(likesUrl);

                if (!likesResponse.IsSuccessStatusCode)
                {
                    var errorContent = await likesResponse.Content.ReadAsStringAsync();
                    _logger.LogWarning("Failed to get post likes. Status: {StatusCode}, Response: {Response}, URL: {Url}", 
                        likesResponse.StatusCode, errorContent, likesUrl);
                    return ApiResult<List<PostLikeUserDto>>.Fail("Failed to get post likes", (HttpStatusCode)likesResponse.StatusCode);
                }

                var likesContent = await likesResponse.Content.ReadAsStringAsync();
                var likesApiResult = JsonSerializer.Deserialize<ApiResult<IEnumerable<LikesDto>>>(likesContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (likesApiResult?.IsSuccess != true || likesApiResult.Data == null || !likesApiResult.Data.Any())
                {
                    return ApiResult<List<PostLikeUserDto>>.Success(new List<PostLikeUserDto>());
                }

                // Convert to list for easier manipulation
                var likesList = likesApiResult.Data.ToList();

                // Get user IDs from likes
                var userIds = likesList.Select(l => l.UserId).Distinct().ToList();

                // Get user profiles using GetUserProfilesByIds
                var userProfilesResult = await _userProfileService.GetUserProfilesByIdsAsync(userIds);
                
                if (!userProfilesResult.IsSuccess || userProfilesResult.Data == null)
                {
                    _logger.LogWarning("Failed to get user profiles for likes");
                    return ApiResult<List<PostLikeUserDto>>.Success(new List<PostLikeUserDto>());
                }

                // Create a dictionary for quick lookup
                var userProfilesDict = userProfilesResult.Data.ToDictionary(u => u.Id, u => u);

                // Map likes to PostLikeUserDto
                var result = likesList
                    .Where(like => userProfilesDict.ContainsKey(like.UserId))
                    .Select(like =>
                    {
                        var userProfile = userProfilesDict[like.UserId];
                        return new PostLikeUserDto
                        {
                            LikeId = like.Id,
                            UserId = like.UserId,
                            PostId = like.PostId,
                            FirstName = userProfile.FirstName,
                            LastName = userProfile.LastName,
                            ProfileImage = null, // Profile image will be handled in frontend using /img/profilephoto.jpg
                            CreatedAt = like.CreatedAt
                        };
                    })
                    .ToList();

                return ApiResult<List<PostLikeUserDto>>.Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching post likes with users for PostId: {PostId}", postId);
                return ApiResult<List<PostLikeUserDto>>.Fail(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}

