namespace LawyerBasket.Gateway.Api.Dtos
{
    public class PostLikeUserDto
    {
        public string LikeId { get; set; } = default!;
        public string UserId { get; set; } = default!;
        public string PostId { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string? ProfileImage { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

