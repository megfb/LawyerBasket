namespace LawyerBasket.Shared.Common.Domain
{
  public interface IUnitOfWork
  {
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

  }
}
