using LawyerBasket.SocialService.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LawyerBasket.SocialService.Api.Domain.Repositories.Configurations
{
    public class FriendConnectionConfiguration : IEntityTypeConfiguration<FriendConnection>
    {
        public void Configure(EntityTypeBuilder<FriendConnection> builder)
        {
            builder.HasKey(fc => fc.Id);

        }
    }
}
