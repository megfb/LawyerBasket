using LawyerBasket.SocialService.Api.Domain.Contracts.Data;
using LawyerBasket.SocialService.Api.Domain.Entities;
using LawyerBasket.SocialService.Api.Domain.Repositories.EntityFramework.DbContexts;

namespace LawyerBasket.SocialService.Api.Domain.Repositories.EntityFramework
{
    public class FriendshipRepository(AppDbContext appDbContext) : GenericRepository<Friendship>(appDbContext), IFriendshipRepository
    {
    }
}
