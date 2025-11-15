using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LawyerBasket.ProfileService.Data.Academy
{
    public class AcademyConfiguration : IEntityTypeConfiguration<Domain.Entities.Academy>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Academy> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.University).IsRequired().HasMaxLength(200);
            builder.Property(a => a.Degree).IsRequired(false).HasMaxLength(100);
            builder.Property(a => a.Department).IsRequired(false).HasMaxLength(200);
            builder.Property(a => a.StartDate).IsRequired();
            builder.Property(a => a.EndDate).IsRequired(false);

            builder.HasOne(a => a.LawyerProfile).WithMany(a => a.Academy).HasForeignKey(a => a.LawyerProfileId);


        }
    }
}
