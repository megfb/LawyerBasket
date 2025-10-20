namespace LawyerBasket.ProfileService.Application.Contracts.Data
{
  public interface IUserProfileRepository : IGenericRepository<Domain.Entities.UserProfile>
  {
    Task<bool> AnyByEmail(string email);
  }
}
