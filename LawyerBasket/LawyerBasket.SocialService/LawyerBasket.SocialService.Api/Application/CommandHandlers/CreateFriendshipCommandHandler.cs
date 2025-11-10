using AutoMapper;
using LawyerBasket.Shared.Common.Domain;
using LawyerBasket.Shared.Common.Response;
using LawyerBasket.SocialService.Api.Application.Commands;
using LawyerBasket.SocialService.Api.Domain.Contracts.Data;
using LawyerBasket.SocialService.Api.Domain.Entities;
using MediatR;

namespace LawyerBasket.SocialService.Api.Application.CommandHandlers
{
    public class CreateFriendshipCommandHandler : IRequestHandler<CreateFriendshipCommand, ApiResult>
    {
        private readonly IFriendshipRepository _friendshipRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateFriendshipCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IFriendConnectionRepository _friendConnectionRepository;
        public CreateFriendshipCommandHandler(IFriendConnectionRepository friendConnectionRepository, IFriendshipRepository friendshipRepository, IUnitOfWork unitOfWork, IMapper mapper, ILogger<CreateFriendshipCommandHandler> logger)
        {
            _friendshipRepository = friendshipRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _friendConnectionRepository = friendConnectionRepository;
        }
        public async Task<ApiResult> Handle(CreateFriendshipCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling CreateFriendshipCommand for UserAId: {UserAId}, UserBId: {UserBId}", request.UserAId, request.UserBId);
            try
            {
                var connection = await _friendConnectionRepository.GetByStatusAsync(request.UserBId, request.UserAId, Status.Pending);
                if (connection == null)
                {
                    _logger.LogWarning("No pending friend connection found from {UserBId} to {UserAId}", request.UserBId, request.UserAId);
                    return ApiResult.Fail("No pending friend connection found.", System.Net.HttpStatusCode.NotFound);
                }
                connection.Status = Status.Accepted;
                connection.AcceptedDate = DateTime.UtcNow;
                connection.UpdatedAt = DateTime.UtcNow;
                _logger.LogInformation("Updating friend connection status to Accepted for connectionId: {ConnectionId}", connection.Id);
                _friendConnectionRepository.Update(connection);
                _logger.LogInformation("Creating friendship between UserAId: {UserAId} and UserBId: {UserBId}", request.UserAId, request.UserBId);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                var friendship = new Friendship
                {
                    Id = Guid.NewGuid().ToString(),
                    UserAId = request.UserAId,
                    UserBId = request.UserBId,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                };
                _logger.LogInformation("Friendship object created: {@Friendship}", friendship);
                await _friendshipRepository.CreateAsync(friendship);
                _logger.LogInformation("Friendship object added to repository");
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("Friendship created successfully between UserAId: {UserAId} and UserBId: {UserBId}", request.UserAId, request.UserBId);
                return ApiResult.Success(System.Net.HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating friendship between UserAId: {UserAId} and UserBId: {UserBId}", request.UserAId, request.UserBId);
                return ApiResult.Fail("An error occurred while creating the friendship.", System.Net.HttpStatusCode.InternalServerError);
            }
            throw new NotImplementedException();
        }
    }
}
