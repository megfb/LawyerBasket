using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LawyerBasket.ProfileService.Data.Gender
{
  public class GenderConfiguration : IEntityTypeConfiguration<Domain.Entities.Gender>
  {
    public void Configure(EntityTypeBuilder<Domain.Entities.Gender> builder)
    {
      builder.HasKey(x => x.Id);
      builder.Property(x => x.Name).IsRequired().HasMaxLength(64);
      builder.Property(x => x.Description).HasMaxLength(256);
      builder.HasMany(x => x.UserProfile).WithOne(x => x.Gender).HasForeignKey(x => x.GenderId);

    }
  }
}
