using LawyerBasket.ProfileService.Domain.Entities;
using LawyerBasket.Shared.Common.Domain;
namespace LawyerBasket.ProfileService.Application.Contracts.Data
{
  public interface ILawyerProfileRepository : IGenericRepository<Domain.Entities.LawyerProfile>
  {
    public Task<bool> BarNumberAny(string barNumber, string? excludeId = null);
    public Task<bool> LicenseNumberAny(string licenseNumber, string? excludeId = null);
    Task<LawyerProfile> GetByUserIdAsync(string id);
  }
}
