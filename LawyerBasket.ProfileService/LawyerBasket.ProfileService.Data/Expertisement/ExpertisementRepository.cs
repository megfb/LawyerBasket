using LawyerBasket.ProfileService.Application.Contracts.Data;

namespace LawyerBasket.ProfileService.Data.Expertisement
{
    public class ExpertisementRepository(AppDbContext appDbContext) : GenericRepository<Domain.Entities.Expertisement>(appDbContext), IExpertisemenetRepository
    {
    }
}
