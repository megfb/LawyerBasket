using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;

namespace LawyerBasket.PostService.Data.Post
{
  public class PostConfiguration : IEntityTypeConfiguration<Domain.Entities.Post>
  {
    public void Configure(EntityTypeBuilder<Domain.Entities.Post> builder)
    {
      builder.ToCollection("posts");
      builder.HasKey(p => p.Id);
      builder.Property(p => p.Content).HasElementName("content").HasMaxLength(255).IsRequired();
      builder.Property(p => p.CreatedAt).HasElementName("createdAt").IsRequired();
      builder.Property(p => p.UpdatedAt).HasElementName("updatedAt").IsRequired(false);

      builder.OwnsMany(p => p.Comments, comments =>
      {
        comments.HasElementName("comments");
        comments.Property(x => x.UserId).HasElementName("userId").IsRequired();
        comments.Property(x => x.Text).HasElementName("text").HasMaxLength(255).IsRequired();
      });
      // Gömülü Comments koleksiyonu
      //builder.OwnsMany(p => p.Comments, comments =>
      //{
      //  comments.HasElementName("comments");
      //  comments.Property(c => c.UserId).HasElementName("userId").IsRequired();
      //  comments.Property(c => c.Text).HasElementName("text").HasMaxLength(255).IsRequired();

      //});


    }
  }
}
