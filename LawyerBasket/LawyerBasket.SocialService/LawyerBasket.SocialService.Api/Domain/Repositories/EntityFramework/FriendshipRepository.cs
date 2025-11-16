using LawyerBasket.SocialService.Api.Domain.Contracts.Data;
using LawyerBasket.SocialService.Api.Domain.Entities;
using LawyerBasket.SocialService.Api.Domain.Repositories.EntityFramework.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace LawyerBasket.SocialService.Api.Domain.Repositories.EntityFramework
{
    public class FriendshipRepository(AppDbContext appDbContext) : GenericRepository<Friendship>(appDbContext), IFriendshipRepository
    {
        public async Task<List<Friendship>> GetActiveFriendshipsByUserIdAsync(string userId)
        {
            return await appDbContext.Friendship
                .Where(f => (f.UserAId == userId || f.UserBId == userId) 
                         && f.IsActive == true 
                         && f.EndedAt == null)
                .ToListAsync();
        }

        public async Task<Friendship?> GetFriendshipByIdAsync(string friendshipId)
        {
            return await appDbContext.Friendship
                .FirstOrDefaultAsync(f => f.Id == friendshipId);
        }
    }
}
