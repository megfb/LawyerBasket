using LawyerBasket.Gateway.Api.Dtos;

namespace LawyerBasket.Gateway.Api.Services
{
    public interface IPostService
    {
        Task<List<PostDto>?> GetUserPostsAsync();
    }
}

