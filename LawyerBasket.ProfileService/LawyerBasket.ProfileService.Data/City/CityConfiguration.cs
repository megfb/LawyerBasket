using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LawyerBasket.ProfileService.Data.City
{
    public class CityConfiguration : IEntityTypeConfiguration<Domain.Entities.City>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.City> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).IsRequired().HasMaxLength(200);

            builder.HasMany(c => c.Address).WithOne(a => a.City).HasForeignKey(a => a.CityId).OnDelete(DeleteBehavior.Cascade);

            builder.HasData(
                new Domain.Entities.City
                {
                    Id = "b1e2c3d4-0001-4f5a-8c9b-1a2b3c4d5e6f",
                    Name = "İstanbul",
                    CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc),
                },
                new Domain.Entities.City
                {
                    Id = "b1e2c3d4-0002-4f5a-8c9b-1a2b3c4d5e6f",
                    Name = "Ankara",
                    CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc),
                },
                new Domain.Entities.City
                {
                    Id = "b1e2c3d4-0003-4f5a-8c9b-1a2b3c4d5e6f",
                    Name = "İzmir",
                    CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc),
                },
                new Domain.Entities.City
                {
                    Id = "b1e2c3d4-0004-4f5a-8c9b-1a2b3c4d5e6f",
                    Name = "Bursa",
                    CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc),
                },
                new Domain.Entities.City
                {
                    Id = "b1e2c3d4-0005-4f5a-8c9b-1a2b3c4d5e6f",
                    Name = "Antalya",
                    CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc),
                }
            );

        }
    }
}
