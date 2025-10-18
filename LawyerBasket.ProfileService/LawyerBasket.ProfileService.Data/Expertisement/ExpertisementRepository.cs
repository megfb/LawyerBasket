using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LawyerBasket.ProfileService.Application.Contracts.Data;

namespace LawyerBasket.ProfileService.Data.Expertisement
{
  public class ExpertisementRepository(AppDbContext appDbContext):GenericRepository<Domain.Entities.Expertisement>(appDbContext), IExpertisemenetRepository
  {
  }
}
