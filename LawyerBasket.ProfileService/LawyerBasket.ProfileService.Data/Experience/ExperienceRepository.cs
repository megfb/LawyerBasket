using LawyerBasket.ProfileService.Application.Contracts.Data;

namespace LawyerBasket.ProfileService.Data.Experience
{
  public class ExperienceRepository(AppDbContext context) : GenericRepository<Domain.Entities.Experience>(context), IExperienceRepository
  {
  }
}
