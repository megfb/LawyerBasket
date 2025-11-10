using LawyerBasket.ProfileService.Application.Contracts.Data;

namespace LawyerBasket.ProfileService.Data.Contact
{
    public class ContactRepository(AppDbContext appDbContext) : GenericRepository<Domain.Entities.Contact>(appDbContext), IContactRepository
    {

    }
}
