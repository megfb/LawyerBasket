using LawyerBasket.ProfileService.Application.Contracts.Data;
using Microsoft.EntityFrameworkCore;

namespace LawyerBasket.ProfileService.Data.LawyerProfile
{
  public class LawyerProfileRepository(AppDbContext appDbContext) : GenericRepository<Domain.Entities.LawyerProfile>(appDbContext), ILawyerProfileRepository
  {
    private readonly AppDbContext _context = appDbContext;
    public async Task<bool> BarNumberAny(string barNumber, string? excludeId = null)
    {
      if (string.IsNullOrEmpty(excludeId))
      {
        return await _context.LawyerProfile.AnyAsync(x => x.BarNumber == barNumber);
      }
      return await _context.LawyerProfile.AnyAsync(x => x.BarNumber == barNumber && x.Id != excludeId);
    }

    public async Task<Domain.Entities.LawyerProfile> GetByUserIdAsync(string id)
    {
      return await _context.LawyerProfile.Where(x => x.UserProfileId == id).FirstOrDefaultAsync();
    }

    public async Task<bool> LicenseNumberAny(string licenseNumber, string? excludeId = null)
    {
      if (string.IsNullOrEmpty(excludeId))
      {
        return await _context.LawyerProfile.AnyAsync(x => x.LicenseNumber == licenseNumber);
      }
      return await _context.LawyerProfile.AnyAsync(x => x.LicenseNumber == licenseNumber && x.Id != excludeId);
    }
  }
}
