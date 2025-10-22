using LawyerBasket.ProfileService.Application.Contracts.Data;
using Microsoft.EntityFrameworkCore;

namespace LawyerBasket.ProfileService.Data.Academy
{
  public class AcademyRepository(AppDbContext appDbContext) : GenericRepository<Domain.Entities.Academy>(appDbContext), IAcademyRepository
  {
    public async Task<IEnumerable<Domain.Entities.Academy>> GetAllByLawyerIdAsync(string id)
    {
      return await appDbContext.Academy.Where(x => x.LawyerProfileId == id).ToListAsync();
    }
  }

}
