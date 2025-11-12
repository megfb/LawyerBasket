using LawyerBasket.ProfileService.Application.Contracts.Data;
using Microsoft.EntityFrameworkCore;

namespace LawyerBasket.ProfileService.Data.LawyerExpertisement
{
    public class LawyerExpertisementRepository(AppDbContext appDbContext) : GenericRepository<Domain.Entities.LawyerExpertisement>(appDbContext), ILawyerExpertisementRepository
    {
        public async Task<IEnumerable<Domain.Entities.LawyerExpertisement>> GetAllByLawyerProfileIdAsync(string id)
        {
            return await appDbContext.LawyerExpertisement.Where(x => x.LawyerProfileId == id).ToListAsync();
        }

        public async Task CreateRangeAsync(IEnumerable<Domain.Entities.LawyerExpertisement> entities)
        {
            await appDbContext.LawyerExpertisement.AddRangeAsync(entities);
        }
    }
}
