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
    }
  }
}
