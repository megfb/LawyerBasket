using AutoMapper;
using LawyerBasket.AuthService.Application.Commands;
using LawyerBasket.AuthService.Application.Contracts.Data;
using LawyerBasket.AuthService.Application.Dtos;
using LawyerBasket.AuthService.Domain.Entities;
using LawyerBasket.Shared.Common.Domain;
using LawyerBasket.Shared.Common.Response;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawyerBasket.AuthService.Application.CommandHandlers
{
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, ApiResult<AppRoleDto>>
    {
        private readonly IAppRoleRepository _appRoleRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateRoleCommandHandler> _logger;
        public CreateRoleCommandHandler(IAppRoleRepository appRoleRepository, IUnitOfWork unitOfWork, IMapper mapper, ILogger<CreateRoleCommandHandler> logger)
        {
            _appRoleRepository = appRoleRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<ApiResult<AppRoleDto>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("CreateRoleCommand started. RoleName: {RoleName}", request.Name);

            try
            {
                if (await _appRoleRepository.Any(request.Name))
                {
                    _logger.LogWarning("Attempt to create duplicate role: {RoleName}", request.Name);
                    return ApiResult<AppRoleDto>.Fail("Role already exists");
                }

                var role = new AppRole
                {
                    Name = request.Name,
                    Description = request.Description,
                    CreatedAt = DateTime.UtcNow
                };

                _logger.LogInformation("Creating new role: {RoleName}", request.Name);
                await _appRoleRepository.CreateAsync(role);

                _logger.LogInformation("Saving changes to the database.");
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("Role created successfully. RoleId: {RoleId}", role.Id);


                _logger.LogInformation("CreateRoleCommand completed successfully. RoleName: {RoleName}", request.Name);
                return ApiResult<AppRoleDto>.Success(_mapper.Map<AppRoleDto>(role));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating role: {RoleName}", request.Name);
                return ApiResult<AppRoleDto>.Fail("An unexpected error occurred");
            }


        }
    }
}
