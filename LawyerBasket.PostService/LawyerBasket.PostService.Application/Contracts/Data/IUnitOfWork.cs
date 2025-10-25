namespace LawyerBasket.PostService.Application.Contracts.Data
{
  public interface IUnitOfWork
  {
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

  }
}
