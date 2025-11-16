using LawyerBasket.Shared.Common.Domain;
using LawyerBasket.Shared.Common.Response;
using LawyerBasket.SocialService.Api.Application.Commands;
using LawyerBasket.SocialService.Api.Domain.Contracts.Application;
using LawyerBasket.SocialService.Api.Domain.Contracts.Data;
using MediatR;

namespace LawyerBasket.SocialService.Api.Application.CommandHandlers
{
    public class DeleteFriendshipCommandHandler : IRequestHandler<DeleteFriendshipCommand, ApiResult>
    {
        private readonly IFriendshipRepository _friendshipRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;
        private readonly ILogger<DeleteFriendshipCommandHandler> _logger;

        public DeleteFriendshipCommandHandler(
            IFriendshipRepository friendshipRepository,
            IUnitOfWork unitOfWork,
            ICurrentUserService currentUserService,
            ILogger<DeleteFriendshipCommandHandler> logger)
        {
            _friendshipRepository = friendshipRepository;
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
            _logger = logger;
        }

        public async Task<ApiResult> Handle(DeleteFriendshipCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
            if (string.IsNullOrEmpty(userId))
            {
                _logger.LogWarning("User ID not found in token.");
                return ApiResult.Fail("User ID not found in token.", System.Net.HttpStatusCode.Unauthorized);
            }

            _logger.LogInformation("Handling DeleteFriendshipCommand for FriendshipId: {FriendshipId}, UserId: {UserId}", 
                request.FriendshipId, userId);

            try
            {
                var friendship = await _friendshipRepository.GetFriendshipByIdAsync(request.FriendshipId);
                
                if (friendship == null)
                {
                    _logger.LogWarning("Friendship not found with Id: {FriendshipId}", request.FriendshipId);
                    return ApiResult.Fail("Friendship not found.", System.Net.HttpStatusCode.NotFound);
                }

                // Check if the user is part of this friendship
                if (friendship.UserAId != userId && friendship.UserBId != userId)
                {
                    _logger.LogWarning("User {UserId} is not authorized to delete friendship {FriendshipId}", 
                        userId, request.FriendshipId);
                    return ApiResult.Fail("You are not authorized to delete this friendship.", 
                        System.Net.HttpStatusCode.Forbidden);
                }

                // Check if friendship is already inactive
                if (!friendship.IsActive)
                {
                    _logger.LogWarning("Friendship {FriendshipId} is already inactive", request.FriendshipId);
                    return ApiResult.Fail("Friendship is already deleted.", System.Net.HttpStatusCode.BadRequest);
                }

                // Soft delete: Set IsActive to false and EndedAt to current time
                friendship.IsActive = false;
                friendship.EndedAt = DateTime.UtcNow;
                friendship.UpdatedAt = DateTime.UtcNow;

                _friendshipRepository.Update(friendship);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Friendship {FriendshipId} deleted successfully by User {UserId}", 
                    request.FriendshipId, userId);
                
                return ApiResult.Success(System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting friendship {FriendshipId} for User {UserId}", 
                    request.FriendshipId, userId);
                return ApiResult.Fail("An error occurred while deleting the friendship.", 
                    System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}

