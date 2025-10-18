using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LawyerBasket.ProfileService.Data.Expertisement
{
  public class ExpertisementConfiguration : IEntityTypeConfiguration<Domain.Entities.Expertisement>
  {
    public void Configure(EntityTypeBuilder<Domain.Entities.Expertisement> builder)
    {
      builder.HasKey(x => x.Id);
      builder.Property(x => x.Name).IsRequired().HasMaxLength(256);
      builder.Property(x => x.Description).IsRequired().HasMaxLength(256);
      builder.HasMany(x => x.lawyerExpertisement).WithOne(x => x.Expertisement).HasForeignKey(x => x.ExpertisementId);
      builder.HasData(
    new Domain.Entities.Expertisement
    {
      Id = "e1a2b3c4-0001-4f5a-8c9d-1a2b3c4d5e6f",
      Name = "Ceza Hukuku",
      Description = "Criminal Law",
      CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc)
    },
    new Domain.Entities.Expertisement
    {
      Id = "e1a2b3c4-0002-4f5a-8c9d-1a2b3c4d5e6f",
      Name = "Ticaret / Şirketler Hukuku",
      Description = "Commercial / Corporate Law",
      CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc)
    },
    new Domain.Entities.Expertisement
    {
      Id = "e1a2b3c4-0003-4f5a-8c9d-1a2b3c4d5e6f",
      Name = "İş Hukuku",
      Description = "Labor / Employment Law",
      CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc)
    },
    new Domain.Entities.Expertisement
    {
      Id = "e1a2b3c4-0004-4f5a-8c9d-1a2b3c4d5e6f",
      Name = "Aile Hukuku",
      Description = "Family Law",
      CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc)
    },
    new Domain.Entities.Expertisement
    {
      Id = "e1a2b3c4-0005-4f5a-8c9d-1a2b3c4d5e6f",
      Name = "Miras Hukuku",
      Description = "Inheritance Law",
      CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc)
    },
    new Domain.Entities.Expertisement
    {
      Id = "e1a2b3c4-0006-4f5a-8c9d-1a2b3c4d5e6f",
      Name = "Gayrimenkul / Emlak Hukuku",
      Description = "Real Estate Law",
      CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc)
    },
    new Domain.Entities.Expertisement
    {
      Id = "e1a2b3c4-0007-4f5a-8c9d-1a2b3c4d5e6f",
      Name = "Fikri Mülkiyet Hukuku",
      Description = "Intellectual Property Law",
      CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc)
    },
    new Domain.Entities.Expertisement
    {
      Id = "e1a2b3c4-0008-4f5a-8c9d-1a2b3c4d5e6f",
      Name = "Vergi Hukuku",
      Description = "Tax Law",
      CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc)
    },
    new Domain.Entities.Expertisement
    {
      Id = "e1a2b3c4-0009-4f5a-8c9d-1a2b3c4d5e6f",
      Name = "İdare Hukuku",
      Description = "Administrative Law",
      CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc)
    },
    new Domain.Entities.Expertisement
    {
      Id = "e1a2b3c4-0010-4f5a-8c9d-1a2b3c4d5e6f",
      Name = "Uluslararası Hukuk",
      Description = "International Law",
      CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc)
    }
);

    }
  }
}
