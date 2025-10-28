using LawyerBasket.Shared.Common.Domain;
using LawyerBasket.SocialService.Api.Domain.Entities;

namespace LawyerBasket.SocialService.Api.Domain.Contracts.Data
{
  public interface IFriendConnectionRepository : IGenericRepository<FriendConnection>
  {
    Task<FriendConnection> GetByStatusAsync(string SenderId, string ReceiverId, Status status);
  }
}
