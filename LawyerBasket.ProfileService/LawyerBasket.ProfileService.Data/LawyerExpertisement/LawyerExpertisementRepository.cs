using LawyerBasket.ProfileService.Application.Contracts.Data;

namespace LawyerBasket.ProfileService.Data.LawyerExpertisement
{
  public class LawyerExpertisementRepository(AppDbContext appDbContext) : GenericRepository<Domain.Entities.LawyerExpertisement>(appDbContext), ILawyerExpertisementRepository
  {
  }
}
