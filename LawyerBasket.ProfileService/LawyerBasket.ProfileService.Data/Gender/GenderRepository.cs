using LawyerBasket.ProfileService.Application.Contracts.Data;

namespace LawyerBasket.ProfileService.Data.Gender
{
    public class GenderRepository(AppDbContext appDbContext) : GenericRepository<Domain.Entities.Gender>(appDbContext), IGenderRepository
    {
    }
}
