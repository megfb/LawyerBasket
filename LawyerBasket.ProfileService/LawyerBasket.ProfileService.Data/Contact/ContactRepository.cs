using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LawyerBasket.ProfileService.Application.Contracts.Data;

namespace LawyerBasket.ProfileService.Data.Contact
{
  public class ContactRepository(AppDbContext dbContext) : GenericRepository<Domain.Entities.Contact>(dbContext), IContactRepository
  {
  }
}
