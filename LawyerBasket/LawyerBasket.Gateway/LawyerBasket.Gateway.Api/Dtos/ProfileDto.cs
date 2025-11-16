namespace LawyerBasket.Gateway.Api.Dtos
{
    public class ProfileDto
    {
        public UserProfileWDetailsDto? UserProfile { get; set; }
        public List<PostDto>? Posts { get; set; }
        public List<PostDto>? CommentedPosts { get; set; }
        public List<PostDto>? LikedPosts { get; set; }
        public List<FriendWithProfileDto>? Friends { get; set; }
    }
}

