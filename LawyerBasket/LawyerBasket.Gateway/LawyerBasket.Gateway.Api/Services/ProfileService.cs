using LawyerBasket.Gateway.Api.Contracts;
using LawyerBasket.Gateway.Api.Dtos;
using LawyerBasket.Shared.Common.Response;
using System.Net;

namespace LawyerBasket.Gateway.Api.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IUserProfileService _userProfileService;
        private readonly IPostService _postService;
        private readonly ICommentService _commentService;
        private readonly ILikesService _likesService;
        private readonly ISocialService _socialService;
        private readonly ILogger<ProfileService> _logger;

        public ProfileService(
            IUserProfileService userProfileService,
            IPostService postService,
            ICommentService commentService,
            ILikesService likesService,
            ISocialService socialService,
            ILogger<ProfileService> logger)
        {
            _userProfileService = userProfileService;
            _postService = postService;
            _commentService = commentService;
            _likesService = likesService;
            _socialService = socialService;
            _logger = logger;
        }

        public async Task<ApiResult<ProfileDto>> GetUserProfileFullAsync()
        {
            try
            {
                // Parallel calls to all services
                var profileTask = _userProfileService.GetUserProfileFullAsync();
                var postsTask = _postService.GetUserPostsAsync();
                var commentedPostsTask = _commentService.GetPostsCommentedByUserAsync();
                var likedPostsTask = _likesService.GetPostsLikedByUserAsync();
                var friendsTask = _socialService.GetUserFriendsWithProfilesAsync();

                await Task.WhenAll(profileTask, postsTask, commentedPostsTask, likedPostsTask, friendsTask);

                var userProfileResult = await profileTask;
                var postsResult = await postsTask;
                var commentedPostsResult = await commentedPostsTask;
                var likedPostsResult = await likedPostsTask;
                var friendsResult = await friendsTask;

                // Check if user profile call failed
                if (!userProfileResult.IsSuccess)
                {
                    return ApiResult<ProfileDto>.Fail(
                        userProfileResult.ErrorMessage ?? new List<string> { "Failed to get user profile" },
                        userProfileResult.Status);
                }

                // Log warnings for other service failures but continue
                if (!postsResult.IsSuccess)
                {
                    _logger.LogWarning("Failed to get posts: {Error}", string.Join(", ", postsResult.ErrorMessage ?? new List<string>()));
                }

                if (!commentedPostsResult.IsSuccess)
                {
                    _logger.LogWarning("Failed to get commented posts: {Error}", string.Join(", ", commentedPostsResult.ErrorMessage ?? new List<string>()));
                }

                if (!likedPostsResult.IsSuccess)
                {
                    _logger.LogWarning("Failed to get liked posts: {Error}", string.Join(", ", likedPostsResult.ErrorMessage ?? new List<string>()));
                }

                if (!friendsResult.IsSuccess)
                {
                    _logger.LogWarning("Failed to get friends: {Error}", string.Join(", ", friendsResult.ErrorMessage ?? new List<string>()));
                }

                var profileDto = new ProfileDto
                {
                    UserProfile = userProfileResult.Data,
                    Posts = postsResult.Data ?? new List<PostDto>(),
                    CommentedPosts = commentedPostsResult.Data ?? new List<PostDto>(),
                    LikedPosts = likedPostsResult.Data ?? new List<PostDto>(),
                    Friends = friendsResult.Data ?? new List<FriendWithProfileDto>()
                };

                return ApiResult<ProfileDto>.Success(profileDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error aggregating profile with posts, commented posts, liked posts and friends");
                return ApiResult<ProfileDto>.Fail(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}

