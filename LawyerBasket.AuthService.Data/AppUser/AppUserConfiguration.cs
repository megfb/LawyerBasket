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

        }
    }
}
