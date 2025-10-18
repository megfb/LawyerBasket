using LawyerBasket.ProfileService.Application.Contracts.Data;

namespace LawyerBasket.ProfileService.Data.LawyerProfile
{
  public class LawyerProfileRepository(AppDbContext appDbContext) : GenericRepository<Domain.Entities.LawyerProfile>(appDbContext), ILawyerProfileRepository
  {
  }
}
