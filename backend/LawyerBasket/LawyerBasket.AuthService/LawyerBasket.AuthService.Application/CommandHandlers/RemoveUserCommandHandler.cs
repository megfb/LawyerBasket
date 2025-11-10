using LawyerBasket.AuthService.Application.Commands;
using LawyerBasket.AuthService.Application.Contracts.Api;
using LawyerBasket.AuthService.Application.Contracts.Data;
using LawyerBasket.Shared.Common.Domain;
using LawyerBasket.Shared.Common.Response;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawyerBasket.AuthService.Application.CommandHandlers
{
    public class RemoveUserCommandHandler : IRequestHandler<RemoveUserCommand, ApiResult>
    {
        private readonly IAppUserRepository _appUserRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<RemoveUserCommandHandler> _logger;
        private readonly ICurrentUserService _currentUserService;
        public RemoveUserCommandHandler(IAppUserRepository appUserRepository, IUnitOfWork unitOfWork, ILogger<RemoveUserCommandHandler> logger, ICurrentUserService currentUserService)
        {
            _appUserRepository = appUserRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _currentUserService = currentUserService;
        }
        public async Task<ApiResult> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
        {
            var UserId = _currentUserService.UserId;
            _logger.LogInformation("RemoveUserCommand is started");

            var user = await _appUserRepository.GetByIdAsync(UserId!);

            if (user is null)
            {
                _logger.LogWarning("User not found");
                return ApiResult.Fail("User not found");
            }

            _logger.LogInformation("User is deleted");
            _appUserRepository.Delete(user);

            _logger.LogInformation("Saving changes to database");
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ApiResult.Success(System.Net.HttpStatusCode.OK);
        }
    }
}
