using AutoMapper;
using LawyerBasket.AuthService.Application.Contracts.Data;
using LawyerBasket.AuthService.Application.Dtos;
using LawyerBasket.AuthService.Application.Queries;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawyerBasket.AuthService.Application.QueryHandlers
{
    public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, ApiResult<List<AppRoleDto>>>
    {
        private readonly IAppRoleRepository _appRoleRepository;
        private readonly ILogger<GetRolesQueryHandler> _logger;
        private readonly IMapper _mapper;
        public GetRolesQueryHandler(IAppRoleRepository appRoleRepository, ILogger<GetRolesQueryHandler> logger, IMapper mapper)
        {
            _appRoleRepository = appRoleRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ApiResult<List<AppRoleDto>>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetRolesQuery started.");
            var roles = await _appRoleRepository.GetAllAsync();
            if (roles is null)
            {
                _logger.LogWarning("No roles found in the system.");
                return ApiResult<List<AppRoleDto>>.Fail("No roles found");
            }

            _logger.LogInformation("GetRolesQuery completed successfully.");
            return ApiResult<List<AppRoleDto>>.Success(_mapper.Map<List<AppRoleDto>>(roles));
        }
    }
}
