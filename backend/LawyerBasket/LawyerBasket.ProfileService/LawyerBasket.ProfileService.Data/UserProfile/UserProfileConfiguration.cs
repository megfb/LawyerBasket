using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LawyerBasket.ProfileService.Data.UserProfile
{
    public class UserProfileConfiguration : IEntityTypeConfiguration<Domain.Entities.UserProfile>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.UserProfile> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(200);
            builder.Property(x => x.PhoneNumber).HasMaxLength(20);
            builder.Property(x => x.BirthDate).IsRequired(false);
            builder.Property(x => x.NationalId).HasMaxLength(50).IsRequired(false);
        }
    }
}
