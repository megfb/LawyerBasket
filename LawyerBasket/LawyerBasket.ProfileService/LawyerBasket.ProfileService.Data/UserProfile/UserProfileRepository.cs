using LawyerBasket.ProfileService.Application.Contracts.Data;
using Microsoft.EntityFrameworkCore;

namespace LawyerBasket.ProfileService.Data.UserProfile
{
  public class UserProfileRepository(AppDbContext dbContext) : GenericRepository<Domain.Entities.UserProfile>(dbContext), IUserProfileRepository
  {
    private readonly AppDbContext _dbContext = dbContext;

    public async Task<bool> AnyByEmail(string email)
    {
      return await _dbContext.UserProfile.AnyAsync(x => x.Email.ToLower() == email.ToLower());
    }

    public async Task<Domain.Entities.UserProfile?> GetFullProfile(string id)
    {
      return await _dbContext.UserProfile.Include(x => x.Address).ThenInclude(x => x.City).Include(x => x.Gender)
        .Include(x => x.LawyerProfile).ThenInclude(x => x.Academy)
        .Include(x => x.LawyerProfile).ThenInclude(x => x.Certificates)
        .Include(x => x.LawyerProfile).ThenInclude(x => x.Contact)
        .Include(x => x.LawyerProfile).ThenInclude(x => x.LawyerExpertisements).ThenInclude(x => x.Expertisement)
        .Include(x => x.LawyerProfile).ThenInclude(x => x.Experience)
        .FirstOrDefaultAsync(x => x.Id == id);


    }

    public async Task<IEnumerable<Domain.Entities.UserProfile>> GetByIdsAsync(IEnumerable<string> ids)
    {
      return await _dbContext.UserProfile
        .Where(x => ids.Contains(x.Id))
        .ToListAsync();
    }
  }
}
