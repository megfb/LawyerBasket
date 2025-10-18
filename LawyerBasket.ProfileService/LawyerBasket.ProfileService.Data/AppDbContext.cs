using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace LawyerBasket.ProfileService.Data
{
  public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
  {
    public DbSet<Domain.Entities.Academy> Academy { get; set; }
    public DbSet<Domain.Entities.City> City { get; set; }
    public DbSet<Domain.Entities.UserProfile> UserProfile { get; set; }
    public DbSet<Domain.Entities.Address> Address { get; set; }
    public DbSet<Domain.Entities.Certificates> Certificates { get; set; }
    public DbSet<Domain.Entities.Contact> Contact { get; set; }
    public DbSet<Domain.Entities.Experience> Experience { get; set; }
    public DbSet<Domain.Entities.Expertisement> Expertisement { get; set; }
    public DbSet<Domain.Entities.LawyerExpertisement> LawyerExpertisement { get; set; }
    public DbSet<Domain.Entities.Gender> Gender { get; set; }
    public DbSet<Domain.Entities.LawyerProfile> LawyerProfile { get; set; }




    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
  }
}
