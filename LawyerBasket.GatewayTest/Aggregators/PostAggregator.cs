using LawyerBasket.GatewayTest.Contracts;
using LawyerBasket.GatewayTest.Dtos.PostService;

namespace LawyerBasket.GatewayTest.Aggregators
{
    public class PostAggregator : IAggregator<IEnumerable<PostDto>>
    {
        private readonly HttpClient _httpClient;
        private readonly string _postUrl;
        public PostAggregator(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _postUrl = configuration[$"PostServiceUrl:PostUrl"]!;
        }
        public async Task<IEnumerable<PostDto>> AggregateAsync(string id)
        {
            var url = $"{_postUrl}/GetPosts/{id}";
            return await _httpClient.GetFromJsonAsync<IEnumerable<PostDto>>(url);
        }
    }
}
