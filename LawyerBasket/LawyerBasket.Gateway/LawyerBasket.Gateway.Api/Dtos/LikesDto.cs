namespace LawyerBasket.Gateway.Api.Dtos
{
    public class LikesDto
    {
        public string Id { get; set; } = default!;
        public string PostId { get; set; } = default!;
        public string UserId { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
    }
}

