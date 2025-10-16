using LawyerBasket.AuthService.Domain.Entities;

namespace LawyerBasket.AuthService.Application.Contracts.Data
{
    public interface IAppRoleRepository : IGenericRepository<AppRole>
    {
        Task<bool> Any(string role);
        Task<AppRole> GetByNameAsync(string name);


    }
}
