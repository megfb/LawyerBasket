using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LawyerBasket.ProfileService.Data.Experience
{
    public class ExperienceConfiguration : IEntityTypeConfiguration<Domain.Entities.Experience>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Experience> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CompanyName).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Position).IsRequired().HasMaxLength(100);
            builder.Property(x => x.StartDate).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(1000);
            builder.Property(x => x.EndDate).IsRequired(false);
            builder.HasOne(x => x.LawyerProfile).WithMany(lp => lp.Experience).HasForeignKey(x => x.LawyerProfileId);


        }
    }
}
