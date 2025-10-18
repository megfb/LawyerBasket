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
    }
  }
}
