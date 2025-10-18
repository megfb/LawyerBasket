using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LawyerBasket.ProfileService.Application.Contracts.Data;
using Microsoft.EntityFrameworkCore;

namespace LawyerBasket.ProfileService.Data.Academy
{
  public class AcademyRepository(AppDbContext appDbContext) : GenericRepository<Domain.Entities.Academy>(appDbContext), IAcademyRepository
  {
  }

}
