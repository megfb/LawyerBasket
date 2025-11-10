using LawyerBasket.SocialService.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace LawyerBasket.SocialService.Api.Domain.Repositories.EntityFramework.DbContexts
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<FriendConnection> FriendConnection { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
