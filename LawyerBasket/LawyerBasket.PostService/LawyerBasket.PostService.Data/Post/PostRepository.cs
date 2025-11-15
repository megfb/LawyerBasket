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

        public async Task<IEnumerable<Domain.Entities.Post>> GetPostsCommentedByUserIdAsync(string userId)
        {
            return await appDbContext.Post
                .Where(p => p.Comments != null && p.Comments.Any(c => c.UserId == userId))
                .ToListAsync();
        }

        public async Task<IEnumerable<Domain.Entities.Post>> GetPostsLikedByUserIdAsync(string userId)
        {
            return await appDbContext.Post
                .Where(p => p.Likes != null && p.Likes.Any(l => l.UserId == userId))
                .ToListAsync();
        }
    }
}
