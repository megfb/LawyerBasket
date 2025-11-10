using LawyerBasket.Shared.Common.Domain;

namespace LawyerBasket.PostService.Domain.Entities
{
    public class Post : Entity
    {
        public string UserId { get; set; } = default!;
        public string Content { get; set; } = default!;
        public List<Likes>? Likes { get; set; } = new List<Likes>();
        public List<Comment>? Comments { get; set; } = new List<Comment>();

    }
}
