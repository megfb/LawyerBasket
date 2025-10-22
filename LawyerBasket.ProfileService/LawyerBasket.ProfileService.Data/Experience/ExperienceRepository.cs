using LawyerBasket.ProfileService.Application.Contracts.Data;
using Microsoft.EntityFrameworkCore;

namespace LawyerBasket.ProfileService.Data.Experience
{
  public class ExperienceRepository(AppDbContext context) : GenericRepository<Domain.Entities.Experience>(context), IExperienceRepository
  {
    public async Task<IEnumerable<Domain.Entities.Experience>> GetAllByLawyerIdAsync(string id)
    {
      return await context.Experience.Where(x => x.LawyerProfileId == id).ToListAsync();
    }
  }
}
