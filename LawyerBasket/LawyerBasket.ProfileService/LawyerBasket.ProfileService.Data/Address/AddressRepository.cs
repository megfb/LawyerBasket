using LawyerBasket.ProfileService.Application.Contracts.Data;
using Microsoft.EntityFrameworkCore;

namespace LawyerBasket.ProfileService.Data.Address
{
  public class AddressRepository(AppDbContext appDbContext) : GenericRepository<Domain.Entities.Address>(appDbContext), IAddressRepository
  {
    private readonly AppDbContext _context = appDbContext;

    public async Task<Domain.Entities.Address> GetByUserIdAsync(string id)
    {
      return await _context.Address.Where(x => x.UserProfileId == id).FirstOrDefaultAsync();
    }
  }
}
