using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LawyerBasket.ProfileService.Data.City
{
  public class CityConfiguration : IEntityTypeConfiguration<Domain.Entities.City>
  {
    public void Configure(EntityTypeBuilder<Domain.Entities.City> builder)
    {
      builder.HasKey(c => c.Id);
      builder.Property(c => c.Name).IsRequired().HasMaxLength(200);

      builder.HasMany(c => c.Address).WithOne(a => a.City).HasForeignKey(a => a.CityId).OnDelete(DeleteBehavior.Cascade);
    }
  }
}
