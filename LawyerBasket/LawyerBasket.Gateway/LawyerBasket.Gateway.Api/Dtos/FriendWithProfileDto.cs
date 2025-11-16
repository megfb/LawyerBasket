namespace LawyerBasket.Gateway.Api.Dtos
{
    public class FriendWithProfileDto
    {
        public string FriendshipId { get; set; } = default!;
        public string FriendUserId { get; set; } = default!;
        public DateTime FriendshipCreatedAt { get; set; }
        public UserProfileDto? Profile { get; set; }
    }
}

