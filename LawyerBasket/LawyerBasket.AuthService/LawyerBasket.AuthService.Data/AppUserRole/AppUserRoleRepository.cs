using LawyerBasket.AuthService.Application.Contracts.Data;

namespace LawyerBasket.AuthService.Data.AppUserRole
{
    public class AppUserRoleRepository(AppDbContext appDbContext) : GenericRepository<Domain.Entities.AppUserRole>(appDbContext), IAppUserRoleRepository
    {
    }
}
