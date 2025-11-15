using LawyerBasket.Gateway.Api.Dtos;

namespace LawyerBasket.Gateway.Api.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IUserProfileService _userProfileService;
        private readonly IPostService _postService;
        private readonly ICommentService _commentService;
        private readonly ILikesService _likesService;
        private readonly ILogger<ProfileService> _logger;

        public ProfileService(
            IUserProfileService userProfileService,
            IPostService postService,
            ICommentService commentService,
            ILikesService likesService,
            ILogger<ProfileService> logger)
        {
            _userProfileService = userProfileService;
            _postService = postService;
            _commentService = commentService;
            _likesService = likesService;
            _logger = logger;
        }

        public async Task<ProfileDto> GetUserProfileFullAsync()
        {
            try
            {
                // Parallel calls to all services
                var profileTask = _userProfileService.GetUserProfileFullAsync();
                var postsTask = _postService.GetUserPostsAsync();
                var commentedPostsTask = _commentService.GetPostsCommentedByUserAsync();
                var likedPostsTask = _likesService.GetPostsLikedByUserAsync();

                await Task.WhenAll(profileTask, postsTask, commentedPostsTask, likedPostsTask);

                var userProfile = await profileTask;
                var posts = await postsTask;
                var commentedPosts = await commentedPostsTask;
                var likedPosts = await likedPostsTask;

                return new ProfileDto
                {
                    UserProfile = userProfile,
                    Posts = posts ?? new List<PostDto>(),
                    CommentedPosts = commentedPosts ?? new List<PostDto>(),
                    LikedPosts = likedPosts ?? new List<PostDto>()
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error aggregating profile with posts, commented posts and liked posts");
                throw;
            }
        }
    }
}

