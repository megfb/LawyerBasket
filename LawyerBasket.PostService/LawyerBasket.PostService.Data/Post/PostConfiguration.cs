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
        comments.HasKey(p => p.Id);
        comments.Property(x => x.UserId).HasElementName("userId").IsRequired();
        comments.Property(x => x.PostId).HasElementName("postId").IsRequired();
        comments.Property(x => x.Text).HasElementName("text").HasMaxLength(255).IsRequired();
      });
      builder.OwnsMany(x => x.Likes, likes =>
      {
        likes.HasElementName("likes");
        likes.HasKey(p => p.Id);
        likes.Property(x => x.UserId).HasElementName("userId").IsRequired();
        likes.Property(x => x.PostId).HasElementName("postId").IsRequired();
      });

    }
  }
}
