using LawyerBasket.PostService.Application.Contracts.Data;
using Microsoft.EntityFrameworkCore;

namespace LawyerBasket.PostService.Data.Post
{
  public class PostRepository(AppDbContext appDbContext) : GenericRepository<Domain.Entities.Post>(appDbContext), IPostRepository
  {
    public async Task<IEnumerable<Domain.Entities.Post>> GetAllByUserIdAsync(string id)
    {
      return await appDbContext.Post.Where(x => x.UserId == id).ToListAsync();
    }
  }
}
