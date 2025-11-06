using LawyerBasket.SocialService.Api.Domain.Contracts.Data;
using LawyerBasket.SocialService.Api.Domain.Entities;
using LawyerBasket.SocialService.Api.Domain.Repositories.EntityFramework.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace LawyerBasket.SocialService.Api.Domain.Repositories.EntityFramework
{
  public class FriendConnectionRepository(AppDbContext appDbContext) : GenericRepository<FriendConnection>(appDbContext), IFriendConnectionRepository
  {
    public Task<FriendConnection?> GetByStatusAsync(string SenderId, string ReceiverId, Status status)
    {
      return appDbContext.FriendConnection.Where(fc => fc.SenderId == SenderId && fc.ReceiverId == ReceiverId && fc.Status == status).FirstOrDefaultAsync();
    }
  }
}
