using LawyerBasket.ProfileService.Domain.Entities;
using LawyerBasket.Shared.Common.Domain;

namespace LawyerBasket.ProfileService.Application.Contracts.Data
{
    public interface ICertificateRepository : IGenericRepository<Domain.Entities.Certificates>
    {
        Task<IEnumerable<Certificates>> GetAllByLawyerIdAsync(string id);
    }
}
