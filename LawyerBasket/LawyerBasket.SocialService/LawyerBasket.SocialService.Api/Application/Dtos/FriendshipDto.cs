namespace LawyerBasket.SocialService.Api.Application.Dtos
{
    public class FriendshipDto
    {
        public string Id { get; set; } = default!;
        public string UserAId { get; set; } = default!;
        public string UserBId { get; set; } = default!;
        public string FriendUserId { get; set; } = default!; // The other user's ID (not the requesting user)
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? EndedAt { get; set; }
    }
}
