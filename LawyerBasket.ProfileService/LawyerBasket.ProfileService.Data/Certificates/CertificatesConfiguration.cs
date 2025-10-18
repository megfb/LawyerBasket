using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LawyerBasket.ProfileService.Data.Certificates
{
  public class CertificatesConfiguration : IEntityTypeConfiguration<Domain.Entities.Certificates>
  {
    public void Configure(EntityTypeBuilder<Domain.Entities.Certificates> builder)
    {
      builder.HasKey(x => x.Id);
      builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
      builder.Property(x => x.Institution).IsRequired().HasMaxLength(200);
      builder.Property(x => x.DateReceived).IsRequired();

      builder.HasOne(x => x.LawyerProfile).WithMany(x => x.Certificates).HasForeignKey(x => x.LawyerProfileId);
    }
  }
}
