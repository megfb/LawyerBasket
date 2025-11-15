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
                new Domain.Entities.City { Id = "b1e2c3d4-0001-4f5a-8c9b-1a2b3c4d5e6f", Name = "Adana", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new Domain.Entities.City { Id = "b1e2c3d4-0002-4f5a-8c9b-1a2b3c4d5e6f", Name = "Adıyaman", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new Domain.Entities.City { Id = "b1e2c3d4-0003-4f5a-8c9b-1a2b3c4d5e6f", Name = "Afyonkarahisar", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new Domain.Entities.City { Id = "b1e2c3d4-0004-4f5a-8c9b-1a2b3c4d5e6f", Name = "Ağrı", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new Domain.Entities.City { Id = "b1e2c3d4-0005-4f5a-8c9b-1a2b3c4d5e6f", Name = "Amasya", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new Domain.Entities.City { Id = "b1e2c3d4-0006-4f5a-8c9b-1a2b3c4d5e6f", Name = "Ankara", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new Domain.Entities.City { Id = "b1e2c3d4-0007-4f5a-8c9b-1a2b3c4d5e6f", Name = "Antalya", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new Domain.Entities.City { Id = "b1e2c3d4-0008-4f5a-8c9b-1a2b3c4d5e6f", Name = "Artvin", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new Domain.Entities.City { Id = "b1e2c3d4-0009-4f5a-8c9b-1a2b3c4d5e6f", Name = "Aydın", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new Domain.Entities.City { Id = "b1e2c3d4-0010-4f5a-8c9b-1a2b3c4d5e6f", Name = "Balıkesir", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new Domain.Entities.City { Id = "b1e2c3d4-0011-4f5a-8c9b-1a2b3c4d5e6f", Name = "Bilecik", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new Domain.Entities.City { Id = "b1e2c3d4-0012-4f5a-8c9b-1a2b3c4d5e6f", Name = "Bingöl", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new Domain.Entities.City { Id = "b1e2c3d4-0013-4f5a-8c9b-1a2b3c4d5e6f", Name = "Bitlis", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new Domain.Entities.City { Id = "b1e2c3d4-0014-4f5a-8c9b-1a2b3c4d5e6f", Name = "Bolu", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new Domain.Entities.City { Id = "b1e2c3d4-0015-4f5a-8c9b-1a2b3c4d5e6f", Name = "Burdur", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new Domain.Entities.City { Id = "b1e2c3d4-0016-4f5a-8c9b-1a2b3c4d5e6f", Name = "Bursa", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new Domain.Entities.City { Id = "b1e2c3d4-0017-4f5a-8c9b-1a2b3c4d5e6f", Name = "Çanakkale", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new Domain.Entities.City { Id = "b1e2c3d4-0018-4f5a-8c9b-1a2b3c4d5e6f", Name = "Çankırı", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new Domain.Entities.City { Id = "b1e2c3d4-0019-4f5a-8c9b-1a2b3c4d5e6f", Name = "Çorum", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new Domain.Entities.City { Id = "b1e2c3d4-0020-4f5a-8c9b-1a2b3c4d5e6f", Name = "Denizli", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new Domain.Entities.City { Id = "b1e2c3d4-0021-4f5a-8c9b-1a2b3c4d5e6f", Name = "Diyarbakır", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new Domain.Entities.City { Id = "b1e2c3d4-0022-4f5a-8c9b-1a2b3c4d5e6f", Name = "Edirne", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new Domain.Entities.City { Id = "b1e2c3d4-0023-4f5a-8c9b-1a2b3c4d5e6f", Name = "Elazığ", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new Domain.Entities.City { Id = "b1e2c3d4-0024-4f5a-8c9b-1a2b3c4d5e6f", Name = "Erzincan", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new Domain.Entities.City { Id = "b1e2c3d4-0025-4f5a-8c9b-1a2b3c4d5e6f", Name = "Erzurum", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new Domain.Entities.City { Id = "b1e2c3d4-0026-4f5a-8c9b-1a2b3c4d5e6f", Name = "Eskişehir", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new Domain.Entities.City { Id = "b1e2c3d4-0027-4f5a-8c9b-1a2b3c4d5e6f", Name = "Gaziantep", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new Domain.Entities.City { Id = "b1e2c3d4-0028-4f5a-8c9b-1a2b3c4d5e6f", Name = "Giresun", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new Domain.Entities.City { Id = "b1e2c3d4-0029-4f5a-8c9b-1a2b3c4d5e6f", Name = "Gümüşhane", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new Domain.Entities.City { Id = "b1e2c3d4-0030-4f5a-8c9b-1a2b3c4d5e6f", Name = "Hakkari", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new Domain.Entities.City { Id = "b1e2c3d4-0031-4f5a-8c9b-1a2b3c4d5e6f", Name = "Hatay", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new Domain.Entities.City { Id = "b1e2c3d4-0032-4f5a-8c9b-1a2b3c4d5e6f", Name = "Isparta", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new Domain.Entities.City { Id = "b1e2c3d4-0033-4f5a-8c9b-1a2b3c4d5e6f", Name = "Mersin", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new Domain.Entities.City { Id = "b1e2c3d4-0034-4f5a-8c9b-1a2b3c4d5e6f", Name = "İstanbul", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new Domain.Entities.City { Id = "b1e2c3d4-0035-4f5a-8c9b-1a2b3c4d5e6f", Name = "İzmir", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new Domain.Entities.City { Id = "b1e2c3d4-0036-4f5a-8c9b-1a2b3c4d5e6f", Name = "Kars", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new Domain.Entities.City { Id = "b1e2c3d4-0037-4f5a-8c9b-1a2b3c4d5e6f", Name = "Kastamonu", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new Domain.Entities.City { Id = "b1e2c3d4-0038-4f5a-8c9b-1a2b3c4d5e6f", Name = "Kayseri", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new Domain.Entities.City { Id = "b1e2c3d4-0039-4f5a-8c9b-1a2b3c4d5e6f", Name = "Kırklareli", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new Domain.Entities.City { Id = "b1e2c3d4-0040-4f5a-8c9b-1a2b3c4d5e6f", Name = "Kırşehir", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new Domain.Entities.City { Id = "b1e2c3d4-0041-4f5a-8c9b-1a2b3c4d5e6f", Name = "Kocaeli", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new Domain.Entities.City { Id = "b1e2c3d4-0042-4f5a-8c9b-1a2b3c4d5e6f", Name = "Konya", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new Domain.Entities.City { Id = "b1e2c3d4-0043-4f5a-8c9b-1a2b3c4d5e6f", Name = "Kütahya", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new Domain.Entities.City { Id = "b1e2c3d4-0044-4f5a-8c9b-1a2b3c4d5e6f", Name = "Malatya", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new Domain.Entities.City { Id = "b1e2c3d4-0045-4f5a-8c9b-1a2b3c4d5e6f", Name = "Manisa", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new Domain.Entities.City { Id = "b1e2c3d4-0046-4f5a-8c9b-1a2b3c4d5e6f", Name = "Kahramanmaraş", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new Domain.Entities.City { Id = "b1e2c3d4-0047-4f5a-8c9b-1a2b3c4d5e6f", Name = "Mardin", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new Domain.Entities.City { Id = "b1e2c3d4-0048-4f5a-8c9b-1a2b3c4d5e6f", Name = "Muğla", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new Domain.Entities.City { Id = "b1e2c3d4-0049-4f5a-8c9b-1a2b3c4d5e6f", Name = "Muş", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new Domain.Entities.City { Id = "b1e2c3d4-0050-4f5a-8c9b-1a2b3c4d5e6f", Name = "Nevşehir", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new Domain.Entities.City { Id = "b1e2c3d4-0051-4f5a-8c9b-1a2b3c4d5e6f", Name = "Niğde", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new Domain.Entities.City { Id = "b1e2c3d4-0052-4f5a-8c9b-1a2b3c4d5e6f", Name = "Ordu", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new Domain.Entities.City { Id = "b1e2c3d4-0053-4f5a-8c9b-1a2b3c4d5e6f", Name = "Rize", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new Domain.Entities.City { Id = "b1e2c3d4-0054-4f5a-8c9b-1a2b3c4d5e6f", Name = "Sakarya", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new Domain.Entities.City { Id = "b1e2c3d4-0055-4f5a-8c9b-1a2b3c4d5e6f", Name = "Samsun", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new Domain.Entities.City { Id = "b1e2c3d4-0056-4f5a-8c9b-1a2b3c4d5e6f", Name = "Siirt", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new Domain.Entities.City { Id = "b1e2c3d4-0057-4f5a-8c9b-1a2b3c4d5e6f", Name = "Sinop", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new Domain.Entities.City { Id = "b1e2c3d4-0058-4f5a-8c9b-1a2b3c4d5e6f", Name = "Sivas", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new Domain.Entities.City { Id = "b1e2c3d4-0059-4f5a-8c9b-1a2b3c4d5e6f", Name = "Tekirdağ", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new Domain.Entities.City { Id = "b1e2c3d4-0060-4f5a-8c9b-1a2b3c4d5e6f", Name = "Tokat", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new Domain.Entities.City { Id = "b1e2c3d4-0061-4f5a-8c9b-1a2b3c4d5e6f", Name = "Trabzon", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new Domain.Entities.City { Id = "b1e2c3d4-0062-4f5a-8c9b-1a2b3c4d5e6f", Name = "Tunceli", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new Domain.Entities.City { Id = "b1e2c3d4-0063-4f5a-8c9b-1a2b3c4d5e6f", Name = "Şanlıurfa", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new Domain.Entities.City { Id = "b1e2c3d4-0064-4f5a-8c9b-1a2b3c4d5e6f", Name = "Uşak", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new Domain.Entities.City { Id = "b1e2c3d4-0065-4f5a-8c9b-1a2b3c4d5e6f", Name = "Van", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new Domain.Entities.City { Id = "b1e2c3d4-0066-4f5a-8c9b-1a2b3c4d5e6f", Name = "Yozgat", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new Domain.Entities.City { Id = "b1e2c3d4-0067-4f5a-8c9b-1a2b3c4d5e6f", Name = "Zonguldak", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new Domain.Entities.City { Id = "b1e2c3d4-0068-4f5a-8c9b-1a2b3c4d5e6f", Name = "Aksaray", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new Domain.Entities.City { Id = "b1e2c3d4-0069-4f5a-8c9b-1a2b3c4d5e6f", Name = "Bayburt", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new Domain.Entities.City { Id = "b1e2c3d4-0070-4f5a-8c9b-1a2b3c4d5e6f", Name = "Karaman", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new Domain.Entities.City { Id = "b1e2c3d4-0071-4f5a-8c9b-1a2b3c4d5e6f", Name = "Kırıkkale", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new Domain.Entities.City { Id = "b1e2c3d4-0072-4f5a-8c9b-1a2b3c4d5e6f", Name = "Batman", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new Domain.Entities.City { Id = "b1e2c3d4-0073-4f5a-8c9b-1a2b3c4d5e6f", Name = "Şırnak", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new Domain.Entities.City { Id = "b1e2c3d4-0074-4f5a-8c9b-1a2b3c4d5e6f", Name = "Bartın", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new Domain.Entities.City { Id = "b1e2c3d4-0075-4f5a-8c9b-1a2b3c4d5e6f", Name = "Ardahan", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new Domain.Entities.City { Id = "b1e2c3d4-0076-4f5a-8c9b-1a2b3c4d5e6f", Name = "Iğdır", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new Domain.Entities.City { Id = "b1e2c3d4-0077-4f5a-8c9b-1a2b3c4d5e6f", Name = "Yalova", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new Domain.Entities.City { Id = "b1e2c3d4-0078-4f5a-8c9b-1a2b3c4d5e6f", Name = "Karabük", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new Domain.Entities.City { Id = "b1e2c3d4-0079-4f5a-8c9b-1a2b3c4d5e6f", Name = "Kilis", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new Domain.Entities.City { Id = "b1e2c3d4-0080-4f5a-8c9b-1a2b3c4d5e6f", Name = "Osmaniye", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
                new Domain.Entities.City { Id = "b1e2c3d4-0081-4f5a-8c9b-1a2b3c4d5e6f", Name = "Düzce", CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc) }
            );

        }
    }
}
