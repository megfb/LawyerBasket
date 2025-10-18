using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LawyerBasket.ProfileService.Application.Contracts.Data;

namespace LawyerBasket.ProfileService.Data.Experience
{
  public class ExperienceRepository(AppDbContext context) : GenericRepository<Domain.Entities.Experience>(context), IExperienceRepository
  {
  }
}
