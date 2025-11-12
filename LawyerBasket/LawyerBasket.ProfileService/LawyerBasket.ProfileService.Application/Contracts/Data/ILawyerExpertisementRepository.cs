using LawyerBasket.Shared.Common.Domain;
namespace LawyerBasket.ProfileService.Application.Contracts.Data
{
    public interface ILawyerExpertisementRepository : IGenericRepository<Domain.Entities.LawyerExpertisement>
    {
        Task<IEnumerable<Domain.Entities.LawyerExpertisement>> GetAllByLawyerProfileIdAsync(string id);
        Task CreateRangeAsync(IEnumerable<Domain.Entities.LawyerExpertisement> entities);
    }
}
