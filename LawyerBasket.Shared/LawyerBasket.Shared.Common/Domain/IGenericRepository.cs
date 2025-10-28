namespace LawyerBasket.Shared.Common.Domain
{
  public interface IGenericRepository<T>
  {
    Task<T> CreateAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(string id);
  }
}
