using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LawyerBasket.ProfileService.Application.Contracts.Data;

namespace LawyerBasket.ProfileService.Data.Address
{
  public class AddressRepository(AppDbContext appDbContext) : GenericRepository<Domain.Entities.Address>(appDbContext), IAddressRepository
  {
  }
}
