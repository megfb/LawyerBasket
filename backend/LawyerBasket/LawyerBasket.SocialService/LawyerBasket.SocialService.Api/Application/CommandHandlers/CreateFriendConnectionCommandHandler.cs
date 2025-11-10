using LawyerBasket.Shared.Common.Domain;
using LawyerBasket.Shared.Common.Response;
using LawyerBasket.SocialService.Api.Application.Commands;
using LawyerBasket.SocialService.Api.Domain.Contracts.Data;
using LawyerBasket.SocialService.Api.Domain.Entities;
using MediatR;

namespace LawyerBasket.SocialService.Api.Application.CommandHandlers
{
    public class CreateFriendConnectionCommandHandler : IRequestHandler<CreateFriendConnectionCommand, ApiResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFriendConnectionRepository _friendConnectionRepository;
        private readonly ILogger<CreateFriendConnectionCommandHandler> _logger;
        public CreateFriendConnectionCommandHandler(IUnitOfWork unitOfWork, IFriendConnectionRepository friendConnectionRepository, ILogger<CreateFriendConnectionCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _friendConnectionRepository = friendConnectionRepository;
            _logger = logger;
        }
        public async Task<ApiResult> Handle(CreateFriendConnectionCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating friend connection from {SenderId} to {ReceiverId}", request.SenderId, request.ReceiverId);
            try
            {
                var friendConnection = new Domain.Entities.FriendConnection
                {
                    Id = Guid.NewGuid().ToString(),
                    SenderId = request.SenderId,
                    ReceiverId = request.ReceiverId,
                    Status = Status.Pending,
                    RequestDate = DateTime.UtcNow,
                    CreatedAt = DateTime.UtcNow
                };
                _logger.LogInformation("Friend connection object created: {@FriendConnection}", friendConnection);
                await _friendConnectionRepository.CreateAsync(friendConnection);
                _logger.LogInformation("Friend connection object added to repository");
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("Friend connection created successfully from {SenderId} to {ReceiverId}", request.SenderId, request.ReceiverId);
                return ApiResult.Success(System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating friend connection from {SenderId} to {ReceiverId}: {ErrorMessage}", request.SenderId, request.ReceiverId, ex.Message);
                return ApiResult.Fail("An error occurred while creating the friend connection.", System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
