using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LawyerBasket.ProfileService.Data.LawyerExpertisement
{
  public class LawyerExpertisementConfiguration : IEntityTypeConfiguration<Domain.Entities.LawyerExpertisement>
  {
    public void Configure(EntityTypeBuilder<Domain.Entities.LawyerExpertisement> builder)
    {
      builder.HasKey(e => e.Id);
      builder.HasOne(e => e.LawyerProfile).WithMany(p => p.LawyerExpertisements).HasForeignKey(e => e.LawyerProfileId);

    }
  }
}
