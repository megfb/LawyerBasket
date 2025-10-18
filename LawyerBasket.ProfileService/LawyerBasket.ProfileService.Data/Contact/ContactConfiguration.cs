using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LawyerBasket.ProfileService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LawyerBasket.ProfileService.Data.Contact
{
  public class ContactConfiguration : IEntityTypeConfiguration<Domain.Entities.Contact>
  {
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Domain.Entities.Contact> builder)
    {
      builder.HasKey(c => c.Id);
      builder.Property(c => c.PhoneNumber).IsRequired().HasMaxLength(15);
      builder.Property(c => c.AlternatePhoneNumber).HasMaxLength(15).IsRequired(false);
      builder.Property(c => c.Email).IsRequired().HasMaxLength(100);
      builder.Property(c => c.AlternateEmail).HasMaxLength(100).IsRequired(false);
      builder.Property(c => c.Website).IsRequired(false).HasMaxLength(200);
      builder.HasOne(c => c.LawyerProfile).WithOne(lp => lp.Contact).HasForeignKey<Domain.Entities.Contact>(c => c.LawyerProfileId).OnDelete(DeleteBehavior.Cascade);
      builder.HasIndex(p => p.LawyerProfileId).IsUnique();

    }
  }
}
