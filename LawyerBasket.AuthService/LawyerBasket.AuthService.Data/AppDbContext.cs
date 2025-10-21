using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace LawyerBasket.AuthService.Data
{
  public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
  {

    public DbSet<Domain.Entities.AppUser> AppUser { get; set; }
    public DbSet<Domain.Entities.AppUserRole> AppUserRole { get; set; }
    public DbSet<Domain.Entities.AppRole> AppRole { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
  }
}
