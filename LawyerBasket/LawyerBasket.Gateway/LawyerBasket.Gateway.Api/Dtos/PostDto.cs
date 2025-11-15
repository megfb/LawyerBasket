namespace LawyerBasket.Gateway.Api.Dtos
{
    public class PostDto
    {
        public string Id { get; set; } = default!;
        public string UserId { get; set; } = default!;
        public string Content { get; set; } = default!;
        public List<LikesDto>? Likes { get; set; }
        public List<CommentDto>? Comments { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}

