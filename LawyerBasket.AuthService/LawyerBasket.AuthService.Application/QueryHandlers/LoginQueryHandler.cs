using LawyerBasket.AuthService.Application.Contracts.Data;
using LawyerBasket.AuthService.Application.Contracts.Infrastructure;
using LawyerBasket.AuthService.Application.Dtos;
using LawyerBasket.AuthService.Application.Queries;
using LawyerBasket.Shared.Common.Response;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawyerBasket.AuthService.Application.QueryHandlers
{
  public class LoginQueryHandler : IRequestHandler<LoginQuery, ApiResult<TokenDto>>
  {
    private readonly IAppUserRepository _appUserRepository;
    private readonly ILogger<LoginQueryHandler> _logger;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenService _tokenService;
    public LoginQueryHandler(IAppUserRepository appUserRepository, ILogger<LoginQueryHandler> logger, IPasswordHasher passwordHasher, ITokenService tokenService)
    {
      _appUserRepository = appUserRepository;
      _logger = logger;
      _passwordHasher = passwordHasher;
      _tokenService = tokenService;
    }
    public async Task<ApiResult<TokenDto>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
      _logger.LogInformation("LoginQuery is started");

      var user = await _appUserRepository.GetByEmailAsync(request.Email);
      if (user is null)
      {
        _logger.LogWarning("User not found");
        return ApiResult<TokenDto>.Fail("User not found.");
      }

      if (!_passwordHasher.Verify(request.Password, user.PasswordHash))
      {
        _logger.LogInformation("Password do not match");
        return ApiResult<TokenDto>.Fail("Password do not match");
      }

      var token = _tokenService.CreateToken(user);


      return ApiResult<TokenDto>.Success(token);
    }
  }
}
