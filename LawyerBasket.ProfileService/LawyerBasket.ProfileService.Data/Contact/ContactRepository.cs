using LawyerBasket.ProfileService.Application.Contracts.Data;

namespace LawyerBasket.ProfileService.Data.Contact
{
  public class ContactRepository(AppDbContext dbContext) : GenericRepository<Domain.Entities.Contact>(dbContext), IContactRepository
  {
  }
}
