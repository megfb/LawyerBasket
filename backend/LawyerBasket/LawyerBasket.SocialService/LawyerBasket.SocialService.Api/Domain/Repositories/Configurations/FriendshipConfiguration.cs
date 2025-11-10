using LawyerBasket.SocialService.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LawyerBasket.SocialService.Api.Domain.Repositories.Configurations
{
    public class FriendshipConfiguration : IEntityTypeConfiguration<Friendship>
    {
        public void Configure(EntityTypeBuilder<Friendship> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
