using LawyerBasket.ProfileService.Application.Contracts.Data;

namespace LawyerBasket.ProfileService.Data.City
{
    public class CityRepository(AppDbContext appDbContext) : GenericRepository<Domain.Entities.City>(appDbContext), ICityRepository
    {
    }
}
