using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LawyerBasket.AuthService.Application.Commands;
using LawyerBasket.AuthService.Application.Contracts.Data;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawyerBasket.AuthService.Application.CommandHandlers
{
    public class RemoveUserCommandHandler : IRequestHandler<RemoveUserCommand, ApiResult>
    {
        private readonly IAppUserRepository _appUserRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<RemoveUserCommandHandler> _logger;
        public RemoveUserCommandHandler(IAppUserRepository appUserRepository, IUnitOfWork unitOfWork, ILogger<RemoveUserCommandHandler> logger)
        {
            _appUserRepository = appUserRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<ApiResult> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("RemoveUserCommand is started");

            var user = await _appUserRepository.GetByIdAsync(request.Id);

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
