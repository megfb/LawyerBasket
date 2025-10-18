using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LawyerBasket.ProfileService.Data.Expertisement
{
  public class ExpertisementConfiguration : IEntityTypeConfiguration<Domain.Entities.Expertisement>
  {
    public void Configure(EntityTypeBuilder<Domain.Entities.Expertisement> builder)
    {
      builder.HasKey(x => x.Id);
      builder.Property(x => x.Name).IsRequired().HasMaxLength(256);
      builder.Property(x => x.Description).IsRequired().HasMaxLength(256);
      builder.HasMany(x => x.lawyerExpertisement).WithOne(x => x.Expertisement).HasForeignKey(x => x.ExpertisementId);

    }
  }
}
