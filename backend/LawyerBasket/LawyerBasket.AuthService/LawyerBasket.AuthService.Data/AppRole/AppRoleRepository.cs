using LawyerBasket.AuthService.Application.Contracts.Data;
using Microsoft.EntityFrameworkCore;

namespace LawyerBasket.AuthService.Data.AppRole
{
    public class AppRoleRepository(AppDbContext appDbContext) : GenericRepository<Domain.Entities.AppRole>(appDbContext), IAppRoleRepository
    {
        private readonly DbSet<Domain.Entities.AppRole> _dbset = appDbContext.Set<Domain.Entities.AppRole>();
        public async Task<bool> Any(string role)
        {
            return await _dbset.AnyAsync(r => r.Name == role);
        }

        public async Task<Domain.Entities.AppRole?> GetByNameAsync(string name)
        {
            return await _dbset.FirstOrDefaultAsync(r => r.Name == name);
        }
    }
}
