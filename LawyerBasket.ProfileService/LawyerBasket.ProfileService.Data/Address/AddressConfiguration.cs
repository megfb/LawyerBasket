using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LawyerBasket.ProfileService.Data.Address
{
  public class AddressConfiguration : IEntityTypeConfiguration<Domain.Entities.Address>
  {
    public void Configure(EntityTypeBuilder<Domain.Entities.Address> builder)
    {
      builder.HasKey(a => a.Id);
      builder.Property(a => a.AddressLine).IsRequired().HasMaxLength(250);
      builder.Property(a => a.CityId).IsRequired();

      builder.HasOne(a => a.UserProfile).WithOne(a => a.Address).HasForeignKey<Domain.Entities.Address>(a => a.UserProfileId);
      builder.HasIndex(p => p.UserProfileId).IsUnique();


    }
  }
}
