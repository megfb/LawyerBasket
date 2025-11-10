namespace LawyerBasket.PostService.Application.Dtos
{
    public class CommentDto
    {
        public string Id { get; set; } = default!;
        public string UserId { get; set; } = default!;
        public string PostId { get; set; } = default!;
        public string Text { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
