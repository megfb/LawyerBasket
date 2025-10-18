using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LawyerBasket.ProfileService.Application.Contracts.Data;

namespace LawyerBasket.ProfileService.Data
{
  public class UnitOfWork(AppDbContext appDbContext) : IUnitOfWork
  {

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
      return appDbContext.SaveChangesAsync();
    }
  }
}
