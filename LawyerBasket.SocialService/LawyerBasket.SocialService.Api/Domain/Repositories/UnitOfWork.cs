using LawyerBasket.Shared.Common.Domain;
using LawyerBasket.SocialService.Api.Domain.Repositories.EntityFramework.DbContexts;

namespace LawyerBasket.SocialService.Api.Domain.Repositories
{
  public class UnitOfWork(AppDbContext appDbContext) : IUnitOfWork
  {

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
      return appDbContext.SaveChangesAsync();
    }
  }
}
