using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System.Reflection;

namespace LawyerBasket.PostService.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {

        public DbSet<Domain.Entities.Post> Post { get; set; }
        //public DbSet<Domain.Entities.Comment> Comment { get; set; }
        public static AppDbContext Create(IMongoDatabase database)
        {
            var optionsBuilder =
                new DbContextOptionsBuilder<AppDbContext>().UseMongoDB(database.Client,
                    database.DatabaseNamespace.DatabaseName);

            return new AppDbContext(optionsBuilder.Options);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
