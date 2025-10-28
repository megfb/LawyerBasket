using LawyerBasket.Shared.Common.Domain;
namespace LawyerBasket.ProfileService.Application.Contracts.Data
{
  public interface ILawyerProfileRepository : IGenericRepository<Domain.Entities.LawyerProfile>
  {
    public Task<bool> BarNumberAny(string barNumber);
    public Task<bool> LicenseNumberAny(string licenseNumber);
  }
}
