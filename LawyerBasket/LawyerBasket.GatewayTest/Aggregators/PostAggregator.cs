using LawyerBasket.GatewayTest.Contracts;
using LawyerBasket.GatewayTest.Dtos.PostService;
using LawyerBasket.Shared.Common.Response;

namespace LawyerBasket.GatewayTest.Aggregators
{
  public class PostAggregator : IAggregator<IEnumerable<PostDto>>
  {
    private readonly HttpClient _httpClient;
    private readonly string _postUrl;
    public PostAggregator(HttpClient httpClient, IConfiguration configuration)
    {
      _httpClient = httpClient;
      _postUrl = configuration[$"PostServiceUrls:PostUrl"]!;
    }
    public async Task<IEnumerable<PostDto>> AggregateAsync(string id)
    {
      var url = $"{_postUrl}/GetPosts/{id}";
      var response = await _httpClient.GetFromJsonAsync<ApiResult<IEnumerable<PostDto>>>(url);


      return response?.Data ?? Enumerable.Empty<PostDto>();
    }
  }
}
