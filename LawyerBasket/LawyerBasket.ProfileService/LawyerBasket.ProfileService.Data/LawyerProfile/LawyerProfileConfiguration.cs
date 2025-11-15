using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LawyerBasket.ProfileService.Data.LawyerProfile
{
    public class LawyerProfileConfiguration : IEntityTypeConfiguration<Domain.Entities.LawyerProfile>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.LawyerProfile> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.BarAssociation).IsRequired().HasMaxLength(200);
            builder.Property(x => x.BarNumber).IsRequired().HasMaxLength(50);
            builder.Property(x => x.LicenseNumber).IsRequired().HasMaxLength(50);
            builder.Property(x => x.LicenseDate).IsRequired();
            builder.Property(x => x.About).IsRequired(false).HasMaxLength(2000);

            builder.HasOne(x => x.UserProfile)
                   .WithOne(x => x.LawyerProfile)
                   .HasForeignKey<Domain.Entities.LawyerProfile>(x => x.UserProfileId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasIndex(p => p.UserProfileId).IsUnique();

        }
    }
}
