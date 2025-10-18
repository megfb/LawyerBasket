using LawyerBasket.AuthService.Application.Contracts.Data;

namespace LawyerBasket.AuthService.Data
{
  public class UnitOfWork(AppDbContext appDbContext) : IUnitOfWork
  {

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
      return appDbContext.SaveChangesAsync();
    }
  }
}
