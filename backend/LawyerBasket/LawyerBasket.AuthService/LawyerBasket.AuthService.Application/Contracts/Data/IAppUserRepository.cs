using LawyerBasket.AuthService.Domain.Entities;
using LawyerBasket.Shared.Common.Domain;

namespace LawyerBasket.AuthService.Application.Contracts.Data
{
    public interface IAppUserRepository : IGenericRepository<AppUser>
    {
        Task<bool> Any(string email);
        Task<AppUser?> GetByEmailAsync(string email);
    }
}
