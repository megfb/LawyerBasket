using System.Net;
using AutoMapper;
using LawyerBasket.AuthService.Application.Commands;
using LawyerBasket.AuthService.Application.Contracts.Data;
using LawyerBasket.AuthService.Application.Dtos;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawyerBasket.AuthService.Application.CommandHandlers
{
  public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, ApiResult<AppRoleDto>>
  {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMediator _mediator;
    private readonly ILogger<UpdateRoleCommandHandler> _logger;
    private readonly IAppRoleRepository _appRoleRepository;
    private readonly IMapper _mapper;
    public UpdateRoleCommandHandler(IUnitOfWork unitOfWork, IAppRoleRepository appRoleRepository, IMediator mediator, ILogger<UpdateRoleCommandHandler> logger, IMapper mapper)
    {
      _unitOfWork = unitOfWork;
      _appRoleRepository = appRoleRepository;
      _mediator = mediator;
      _logger = logger;
      _mapper = mapper;
    }
    public async Task<ApiResult<AppRoleDto>> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
      try
      {
        _logger.LogInformation("UpdateRoleCommand started. RoleName: {RoleName}", request.Name);



        if (request.Name == "Admin")
        {
          _logger.LogWarning("Attempt to modify protected role: {RoleName}", request.Name);
          return ApiResult<AppRoleDto>.Fail("This role cannot be modified");
        }

        _logger.LogInformation("Fetching role with ID: {RoleId}", request.Id);
        var role = await _appRoleRepository.GetByIdAsync(request.Id);

        if (role is null)
        {
          _logger.LogWarning("Update failed. Role not found. RoleId: {RoleId}", request.Id);
          return ApiResult<AppRoleDto>.Fail("Role not found", HttpStatusCode.NotFound);
        }

        _logger.LogInformation("Role found. Proceeding with update. RoleId: {RoleId}", request.Id);
        role.Name = request.Name;
        role.Description = request.Description;
        role.UpdatedAt = DateTime.UtcNow;

        _logger.LogInformation("Updating role in repository. RoleId: {RoleId}", request.Id);
        _appRoleRepository.Update(role);

        _logger.LogInformation("Saving changes to the database.");
        await _unitOfWork.SaveChangesAsync(cancellationToken);


        _logger.LogInformation("Role updated successfully. RoleId: {RoleId}", request.Id);
        return ApiResult<AppRoleDto>.Success(_mapper.Map<AppRoleDto>(role));
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error updating role {RoleId}", request.Id);
        return ApiResult<AppRoleDto>.Fail("An unexpected error occurred");
      }

    }
  }
}
