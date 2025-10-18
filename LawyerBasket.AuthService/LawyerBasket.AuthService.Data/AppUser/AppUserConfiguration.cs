using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LawyerBasket.AuthService.Data.AppUser
{
  public class AppUserConfiguration : IEntityTypeConfiguration<Domain.Entities.AppUser>
  {
    public void Configure(EntityTypeBuilder<Domain.Entities.AppUser> builder)
    {
      builder.HasKey(x => x.Id);
      builder.Property(x => x.Email).IsRequired().HasMaxLength(255);
      builder.Property(x => x.PasswordHash).IsRequired();
      builder.HasMany(x => x.AppUserRole).WithOne(x => x.AppUser).HasForeignKey(x => x.RoleId);
      builder.Property(x => x.CreatedAt).IsRequired();
      builder.Property(x => x.UpdatedAt).IsRequired(false);

      builder.HasData(new Domain.Entities.AppUser
      {
        Id = "d9f8c5b2-4a1e-4c3b-9f21-7e2b8c123456",
        Email = "admin@admin.com",
        PasswordHash = "$2a$11$6zFgHTy5N6mMPRIyEiut1ei.PTzGCZA3wZYWgfRToORZP1oW7qBBi",
        CreatedAt = new DateTime(2025, 01, 01, 0, 0, 0, DateTimeKind.Utc)
      });

    }
  }
}
