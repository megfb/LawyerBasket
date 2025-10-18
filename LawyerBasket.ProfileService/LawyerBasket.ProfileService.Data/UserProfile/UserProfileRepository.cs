using LawyerBasket.ProfileService.Application.Contracts.Data;

namespace LawyerBasket.ProfileService.Data.UserProfile
{
  public class UserProfileRepository(AppDbContext dbContext) : GenericRepository<Domain.Entities.UserProfile>(dbContext), IUserProfileRepository
  {
  }
}
