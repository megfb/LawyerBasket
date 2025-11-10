using LawyerBasket.ProfileService.Application.Contracts.Data;
using Microsoft.EntityFrameworkCore;

namespace LawyerBasket.ProfileService.Data.LawyerProfile
{
    public class LawyerProfileRepository(AppDbContext appDbContext) : GenericRepository<Domain.Entities.LawyerProfile>(appDbContext), ILawyerProfileRepository
    {
        private readonly AppDbContext _context = appDbContext;
        public async Task<bool> BarNumberAny(string barNumber)
        {
            return await _context.LawyerProfile.AnyAsync(x => x.BarNumber == barNumber);
        }

        public async Task<bool> LicenseNumberAny(string licenseNumber)
        {
            return await _context.LawyerProfile.AnyAsync(x => x.LicenseNumber == licenseNumber);
        }
    }
}
