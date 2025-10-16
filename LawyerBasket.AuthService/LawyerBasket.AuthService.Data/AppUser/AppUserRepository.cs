using LawyerBasket.AuthService.Application.Contracts.Data;
using Microsoft.EntityFrameworkCore;

namespace LawyerBasket.AuthService.Data.AppUser
{
    public class AppUserRepository(AppDbContext appDbContext) : GenericRepository<Domain.Entities.AppUser>(appDbContext), IAppUserRepository
    {
        private readonly DbSet<Domain.Entities.AppUser> _dbset = appDbContext.Set<Domain.Entities.AppUser>();
        public async Task<bool> Any(string email)
        {
            return await _dbset.AnyAsync(u => u.Email == email);
        }
    }
}
