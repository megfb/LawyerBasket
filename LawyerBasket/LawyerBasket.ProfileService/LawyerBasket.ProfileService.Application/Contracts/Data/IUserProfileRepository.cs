using LawyerBasket.ProfileService.Domain.Entities;
using LawyerBasket.Shared.Common.Domain;
namespace LawyerBasket.ProfileService.Application.Contracts.Data
{
  public interface IUserProfileRepository : IGenericRepository<Domain.Entities.UserProfile>
  {
    Task<bool> AnyByEmail(string email);
    Task<UserProfile?> GetFullProfile(string id);
    Task<IEnumerable<UserProfile>> GetByIdsAsync(IEnumerable<string> ids);
  }
}
