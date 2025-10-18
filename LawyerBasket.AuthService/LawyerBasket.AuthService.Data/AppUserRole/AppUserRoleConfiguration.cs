using Microsoft.EntityFrameworkCore;

namespace LawyerBasket.AuthService.Data.AppUserRole
{
  public class AppUserRoleConfiguration : IEntityTypeConfiguration<Domain.Entities.AppUserRole>
  {
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Domain.Entities.AppUserRole> builder)
    {
      builder.HasKey(x => x.Id);
      builder.HasOne(x => x.AppUser).WithMany(x => x.AppUserRole).HasForeignKey(x => x.UserId);
      builder.HasOne(x => x.AppRole).WithMany(x => x.AppUserRole).HasForeignKey(x => x.RoleId);
      builder.Property(x => x.CreatedAt).IsRequired();
      builder.Property(x => x.UpdatedAt).IsRequired(false);


      builder.HasData(new Domain.Entities.AppUserRole
      {
        Id = "a3f5d2c7-9b8e-4f1a-92d4-6c3e7b8f1a2b",
        UserId = "d9f8c5b2-4a1e-4c3b-9f21-7e2b8c123456",
        RoleId = "8a1f4a29-4f91-4b6b-835b-9c12f89e6f21",
        CreatedAt = new DateTime(2025, 01, 01, 0, 0, 0, DateTimeKind.Utc)
      });
    }
  }
}
