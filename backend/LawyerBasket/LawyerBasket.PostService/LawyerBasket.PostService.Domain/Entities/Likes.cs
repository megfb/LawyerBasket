using LawyerBasket.Shared.Common.Domain;

namespace LawyerBasket.PostService.Domain.Entities
{
    public class Likes : Entity
    {
        public string UserId { get; set; } = default!;
        public string PostId { get; set; } = default!;
    }
}
