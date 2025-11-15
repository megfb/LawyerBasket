using LawyerBasket.Gateway.Api.Dtos;

namespace LawyerBasket.Gateway.Api.Services
{
    public interface ILikesService
    {
        Task<List<PostDto>?> GetPostsLikedByUserAsync();
    }
}

