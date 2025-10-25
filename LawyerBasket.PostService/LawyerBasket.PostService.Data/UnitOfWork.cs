using LawyerBasket.PostService.Application.Contracts.Data;

namespace LawyerBasket.PostService.Data
{
  public class UnitOfWork(AppDbContext appDbContext) : IUnitOfWork
  {

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
      return appDbContext.SaveChangesAsync();
    }
  }
}
