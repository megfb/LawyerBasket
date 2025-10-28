using LawyerBasket.ProfileService.Domain.Entities;
using LawyerBasket.Shared.Common.Domain;

namespace LawyerBasket.ProfileService.Application.Contracts.Data
{
  public interface IAcademyRepository : IGenericRepository<Academy>
  {
    Task<IEnumerable<Academy>> GetAllByLawyerIdAsync(string id);
  }
}
