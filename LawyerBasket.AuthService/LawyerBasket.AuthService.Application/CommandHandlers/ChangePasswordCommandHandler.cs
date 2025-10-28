using AutoMapper;
using LawyerBasket.AuthService.Application.Commands;
using LawyerBasket.AuthService.Application.Contracts.Api;
using LawyerBasket.AuthService.Application.Contracts.Data;
using LawyerBasket.AuthService.Application.Contracts.Infrastructure;
using LawyerBasket.AuthService.Application.Dtos;
using LawyerBasket.Shared.Common.Domain;
using LawyerBasket.Shared.Common.Response;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawyerBasket.AuthService.Application.CommandHandlers
{
  public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, ApiResult<AppUserDto>>
  {
    private readonly IAppUserRepository _appUserRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ChangePasswordCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ICurrentUserService _currentUserService;
    public ChangePasswordCommandHandler(IAppUserRepository appUserRepository, IUnitOfWork unitOfWork, ILogger<ChangePasswordCommandHandler> logger, IMapper mapper, IPasswordHasher passwordHasher, ICurrentUserService currentUserService)
    {
      _appUserRepository = appUserRepository;
      _unitOfWork = unitOfWork;
      _logger = logger;
      _mapper = mapper;
      _passwordHasher = passwordHasher;
      _currentUserService = currentUserService;
    }
    public async Task<ApiResult<AppUserDto>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {

      var UserId = _currentUserService.UserId;
      _logger.LogInformation("Handling ChangePasswordCommand for User Id: {UserId}", UserId);
      var user = await _appUserRepository.GetByIdAsync(UserId);
      if (user is null)
      {
        _logger.LogWarning("User not found");
        return ApiResult<AppUserDto>.Fail("User not found");
      }

      _logger.LogInformation("Password verify is worked");
      var isPasswordValid = _passwordHasher.Verify(request.Password, user.PasswordHash);
      if (!isPasswordValid)
      {
        _logger.LogWarning("Current password is incorrect");
        return ApiResult<AppUserDto>.Fail("Current password is incorrect");
      }

      if (request.NewPassword != request.ReNewPassword)
      {
        _logger.LogWarning("New password and confirmation do not match");
        return ApiResult<AppUserDto>.Fail("New password and confirmation do not match");
      }

      if (request.NewPassword == request.Password)
      {
        _logger.LogWarning("New password cannot be the same as current password");
        return ApiResult<AppUserDto>.Fail("New password cannot be the same as current password");
      }
      try
      {
        _logger.LogInformation("Password hash is worked");
        user.PasswordHash = _passwordHasher.Hash(request.NewPassword);

        _logger.LogInformation("Password is changed");
        _appUserRepository.Update(user);

        _logger.LogInformation("Saved to database");
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var userDto = _mapper.Map<AppUserDto>(user);

        _logger.LogInformation("ChangePasswordHandler is successfully");
        return ApiResult<AppUserDto>.Success(userDto);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error occurred while changing password");
        return ApiResult<AppUserDto>.Fail("An unexpected error occurred while changing the password");
      }


    }
  }
}
