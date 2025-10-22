namespace LawyerBasket.SocialService.Api.Domain.Contracts.Data
{
  public interface IUnitOfWork
  {
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

  }
}
