using AutoMapper;
using LawyerBasket.AuthService.Application.Commands;
using LawyerBasket.AuthService.Application.Contracts.Data;
using LawyerBasket.AuthService.Application.Contracts.Infrastructure;
using LawyerBasket.AuthService.Application.Dtos;
using LawyerBasket.AuthService.Domain.Entities;
using LawyerBasket.Shared.Common.Domain;
using LawyerBasket.Shared.Common.Response;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawyerBasket.AuthService.Application.CommandHandlers
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ApiResult<AppUserDto>>
    {
        private readonly IAppUserRepository _appUserRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<RegisterCommandHandler> _logger;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IAppRoleRepository _appRoleRepository;
        private readonly IMapper _mapper;
        public RegisterCommandHandler(IAppUserRepository appUserRepository, IUnitOfWork unitOfWork, ILogger<RegisterCommandHandler> logger, IPasswordHasher passwordHasher, IAppRoleRepository appRoleRepository, IMapper mapper)
        {
            _appUserRepository = appUserRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _passwordHasher = passwordHasher;
            _appRoleRepository = appRoleRepository;
            _mapper = mapper;
        }
        public async Task<ApiResult<AppUserDto>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("RegisterCommand started. Email: {Email}", request.Email);

                if (await _appUserRepository.Any(request.Email))
                {
                    _logger.LogWarning("Attempt to register with existing email: {Email}", request.Email);
                    return ApiResult<AppUserDto>.Fail("Email already exists");
                }

                var role = await _appRoleRepository.GetByNameAsync("User");
                if (role == null)
                {
                    _logger.LogError("Role 'User' does not exist. Cannot assign role to new user.");
                    return ApiResult<AppUserDto>.Fail("Role 'User' does not exist");
                }

                _logger.LogInformation("Creating new user with email: {Email}", request.Email);
                var user = new AppUser()
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = request.Email.ToLower(),
                    PasswordHash = _passwordHasher.Hash(request.Password),
                    CreatedAt = DateTime.UtcNow,
                    AppUserRole = new List<AppUserRole>()
                    {
                        new AppUserRole
                        {
                            Id = Guid.NewGuid().ToString(),
                            RoleId = role.Id,
                            CreatedAt = DateTime.UtcNow
                        }
                    }
                };

                _logger.LogInformation("Saving new user to the database. Email: {Email}", request.Email);
                await _appUserRepository.CreateAsync(user);

                _logger.LogInformation("New user created successfully. Email: {Email}", request.Email);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("RegisterCommand completed successfully. Email: {Email}", request.Email);
                return ApiResult<AppUserDto>.Success(_mapper.Map<AppUserDto>(user));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while registering user: {Email}", request.Email);
                return ApiResult<AppUserDto>.Fail("An unexpected error occurred");
            }


        }
    }
}
