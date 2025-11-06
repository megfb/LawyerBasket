using LawyerBasket.Shared.Common.Domain;
using LawyerBasket.SocialService.Api.Domain.Repositories.EntityFramework.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace LawyerBasket.SocialService.Api.Domain.Repositories.EntityFramework
{
    public class GenericRepository<T>(AppDbContext context) : IGenericRepository<T> where T : class, IEntity, new()
    {
        private readonly DbSet<T> _dbSet = context.Set<T>();

        public async Task<T> CreateAsync(T entity)
        {
            var entry = await _dbSet.AddAsync(entity);
            return entity;
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(string id)
        {
            return await _dbSet.FindAsync(id);
        }
        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

    }
}
