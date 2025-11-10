using LawyerBasket.ProfileService.Domain.Entities;
using LawyerBasket.Shared.Common.Domain;

namespace LawyerBasket.ProfileService.Application.Contracts.Data
{
  public interface IAddressRepository : IGenericRepository<Domain.Entities.Address>
  {
    Task<Address> GetByUserIdAsync(string id);

  }
}
