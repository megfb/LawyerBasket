using LawyerBasket.Gateway.Api.Contracts;
using LawyerBasket.Gateway.Api.Dtos;
using LawyerBasket.Shared.Common.Response;
using System.Net;
using System.Text.Json;

namespace LawyerBasket.Gateway.Api.Services
{
    public class PostService : IPostService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<PostService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _postServiceUrl;

        public PostService(
            IHttpClientFactory httpClientFactory,
            ILogger<PostService> logger,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _postServiceUrl = _configuration["ServiceUrls:PostService"] 
                ?? throw new InvalidOperationException("ServiceUrls:PostService configuration is missing.");
        }

        public async Task<ApiResult<List<PostDto>>> GetUserPostsAsync()
        {
            try
            {
                // Get token from current request
                var token = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].FirstOrDefault();
                var httpClient = CreateHttpClientWithToken(token);

                // Direct call to PostService (bypassing gateway to avoid circular routing)
                var url = $"{_postServiceUrl}/api/Post/GetPosts";
                var response = await httpClient.GetAsync(url);
                
                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning("Failed to get posts. Status: {StatusCode}", response.StatusCode);
                    return ApiResult<List<PostDto>>.Fail("Failed to get posts", (HttpStatusCode)response.StatusCode);
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
                _logger.LogError(ex, "Error fetching posts");
                return ApiResult<List<PostDto>>.Fail(ex.Message, HttpStatusCode.InternalServerError);
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

