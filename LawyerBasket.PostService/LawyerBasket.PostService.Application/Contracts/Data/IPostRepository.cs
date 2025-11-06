using LawyerBasket.PostService.Domain.Entities;
using LawyerBasket.Shared.Common.Domain;

namespace LawyerBasket.PostService.Application.Contracts.Data
{
    public interface IPostRepository : IGenericRepository<Domain.Entities.Post>
    {
        Task<IEnumerable<Post>> GetAllByUserIdAsync(string id);
    }
}
