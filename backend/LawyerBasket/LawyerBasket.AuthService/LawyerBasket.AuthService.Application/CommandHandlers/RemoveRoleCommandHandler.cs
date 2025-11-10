using LawyerBasket.AuthService.Application.Commands;
using LawyerBasket.AuthService.Application.Contracts.Data;
using LawyerBasket.Shared.Common.Domain;
using LawyerBasket.Shared.Common.Response;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace LawyerBasket.AuthService.Application.CommandHandlers
{
    public class RemoveRoleCommandHandler : IRequestHandler<RemoveRoleCommand, ApiResult>
    {
        private readonly ILogger<RemoveRoleCommandHandler> _logger;
        private readonly IAppRoleRepository _appRoleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveRoleCommandHandler(ILogger<RemoveRoleCommandHandler> logger, IAppRoleRepository appRoleRepository, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _appRoleRepository = appRoleRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiResult> Handle(RemoveRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _appRoleRepository.GetByIdAsync(request.Id);

            if (role is null)
                return ApiResult.Fail("Role not found", HttpStatusCode.NotFound);

            _appRoleRepository.Delete(role);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ApiResult.Success(HttpStatusCode.OK);
        }
    }
}
