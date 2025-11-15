using LawyerBasket.Gateway.Api.Dtos;

namespace LawyerBasket.Gateway.Api.Services
{
    public interface ICommentService
    {
        Task<List<PostDto>?> GetPostsCommentedByUserAsync();
    }
}

