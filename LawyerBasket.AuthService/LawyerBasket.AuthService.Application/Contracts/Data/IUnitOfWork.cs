namespace LawyerBasket.AuthService.Application.Contracts.Data
{
  public interface IUnitOfWork
  {
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

  }
}
