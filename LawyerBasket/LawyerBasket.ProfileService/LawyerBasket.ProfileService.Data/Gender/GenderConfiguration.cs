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

            builder.HasData(
                new Domain.Entities.Gender
                {
                    Id = "c1d2e3f4-0001-4a5b-8c9d-1a2b3c4d5e6f",
                    Name = "Erkek",
                    Description = "Erkek",
                    CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc),
                },
                new Domain.Entities.Gender
                {
                    Id = "c1d2e3f4-0002-4a5b-8c9d-1a2b3c4d5e6f",
                    Name = "Kadın",
                    Description = "Kadın",
                    CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc),
                },
                new Domain.Entities.Gender
                {
                    Id = "c1d2e3f4-0003-4a5b-8c9d-1a2b3c4d5e6f",
                    Name = "Belirtmek İstemiyorum",
                    Description = "Belirtmek İstemiyorum",
                    CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc),
                }
            );

        }
    }
}
