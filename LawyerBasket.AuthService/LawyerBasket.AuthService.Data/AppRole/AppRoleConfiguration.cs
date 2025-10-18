using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LawyerBasket.AuthService.Data.AppRole
{
  public class AppRoleConfiguration : IEntityTypeConfiguration<Domain.Entities.AppRole>
  {
    public void Configure(EntityTypeBuilder<Domain.Entities.AppRole> builder)
    {
      builder.HasKey(x => x.Id);
      builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
      builder.Property(x => x.Description).IsRequired(false).HasMaxLength(500);
      builder.HasMany(x => x.AppUserRole).WithOne(x => x.AppRole).HasForeignKey(x => x.RoleId);
      builder.Property(x => x.CreatedAt).IsRequired();
      builder.Property(x => x.UpdatedAt).IsRequired(false);

      builder.HasData(
          new Domain.Entities.AppRole
          {
            Id = "8a1f4a29-4f91-4b6b-835b-9c12f89e6f21",
            Name = "Admin",
            Description = "Role for Admin",
            CreatedAt = new DateTime(2025, 01, 01, 0, 0, 0, DateTimeKind.Utc)
          },
          new Domain.Entities.AppRole
          {
            Id = "f39d0b0f-4a2e-4f9a-a4da-9b61b0b8c112",
            Name = "User",
            Description = "Role for User",
            CreatedAt = new DateTime(2025, 01, 01, 0, 0, 0, DateTimeKind.Utc)
          },
          new Domain.Entities.AppRole
          {
            Id = "a1a7a79e-5c53-47e8-b44d-19a98f5ac789",
            Name = "Lawyer",
            Description = "Role for lawyers",
            CreatedAt = new DateTime(2025, 01, 01, 0, 0, 0, DateTimeKind.Utc)
          },
          new Domain.Entities.AppRole
          {
            Id = "5f1f0a00-cf2e-4ee8-b0e8-23e3a091cdee",
            Name = "Client",
            Description = "Role for clients",
            CreatedAt = new DateTime(2025, 01, 01, 0, 0, 0, DateTimeKind.Utc)
          }
      );
    }
  }
}
