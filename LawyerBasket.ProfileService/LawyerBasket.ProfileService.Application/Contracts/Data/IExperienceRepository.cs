using LawyerBasket.ProfileService.Domain.Entities;

namespace LawyerBasket.ProfileService.Application.Contracts.Data
{
  public interface IExperienceRepository : IGenericRepository<Domain.Entities.Experience>
  {
    Task<IEnumerable<Experience>> GetAllByLawyerIdAsync(string id);
  }
}
