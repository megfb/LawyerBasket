using LawyerBasket.Shared.Common.Response;
using LawyerBasket.SocialService.Api.Application.Dtos;
using LawyerBasket.SocialService.Api.Application.Queries;
using LawyerBasket.SocialService.Api.Domain.Contracts.Application;
using LawyerBasket.SocialService.Api.Domain.Contracts.Data;
using MediatR;

namespace LawyerBasket.SocialService.Api.Application.QueryHandlers
{
    public class GetUserFriendsQueryHandler : IRequestHandler<GetUserFriendsQuery, ApiResult<List<FriendshipDto>>>
    {
        private readonly IFriendshipRepository _friendshipRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly ILogger<GetUserFriendsQueryHandler> _logger;

        public GetUserFriendsQueryHandler(
            IFriendshipRepository friendshipRepository,
            ICurrentUserService currentUserService,
            ILogger<GetUserFriendsQueryHandler> logger)
        {
            _friendshipRepository = friendshipRepository;
            _currentUserService = currentUserService;
            _logger = logger;
        }

        public async Task<ApiResult<List<FriendshipDto>>> Handle(GetUserFriendsQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
            if (string.IsNullOrEmpty(userId))
            {
                _logger.LogWarning("User ID not found in token.");
                return ApiResult<List<FriendshipDto>>.Fail("User ID not found in token.", System.Net.HttpStatusCode.Unauthorized);
            }

            _logger.LogInformation("Getting friends for UserId: {UserId}", userId);

            try
            {
                var friendships = await _friendshipRepository.GetActiveFriendshipsByUserIdAsync(userId);

                if (friendships == null || !friendships.Any())
                {
                    _logger.LogInformation("No active friendships found for UserId: {UserId}", userId);
                    return ApiResult<List<FriendshipDto>>.Success(new List<FriendshipDto>());
                }

                var friendshipDtos = friendships.Select(f => new FriendshipDto
                {
                    Id = f.Id,
                    UserAId = f.UserAId,
                    UserBId = f.UserBId,
                    FriendUserId = f.UserAId == userId ? f.UserBId : f.UserAId,
                    IsActive = f.IsActive,
                    CreatedAt = f.CreatedAt,
                    EndedAt = f.EndedAt
                }).ToList();

                _logger.LogInformation("Found {Count} active friendships for UserId: {UserId}", friendshipDtos.Count, userId);
                return ApiResult<List<FriendshipDto>>.Success(friendshipDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting friends for UserId: {UserId}", userId);
                return ApiResult<List<FriendshipDto>>.Fail("An error occurred while getting friends.", System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}

