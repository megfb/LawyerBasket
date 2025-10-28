using AutoMapper;
using LawyerBasket.ProfileService.Application.Commands;
using LawyerBasket.ProfileService.Application.Contracts.Api;
using LawyerBasket.ProfileService.Application.Contracts.Data;
using LawyerBasket.ProfileService.Application.Dtos;
using LawyerBasket.ProfileService.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawyerBasket.ProfileService.Application.CommandHandlers
{
  public class CreateUserProfileCommandHandler : IRequestHandler<CreateUserProfileCommand, ApiResult<UserProfileDto>>
  {
    private readonly IUserProfileRepository _userProfileRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateUserProfileCommandHandler> _logger;
    private readonly ICurrentUserService _currentUserService;

    public CreateUserProfileCommandHandler(ICurrentUserService currentUserService, IUserProfileRepository userProfileRepository, IUnitOfWork unitOfWork, IMapper mapper, ILogger<CreateUserProfileCommandHandler> logger)
    {
      _userProfileRepository = userProfileRepository;
      _unitOfWork = unitOfWork;
      _mapper = mapper;
      _logger = logger;
      _currentUserService = currentUserService;
    }

    public async Task<ApiResult<UserProfileDto>> Handle(CreateUserProfileCommand request, CancellationToken cancellationToken)
    {
      try
      {
        _logger.LogInformation("CreateUserProfile started. Email: {Email}", request.Email);

        if (await _userProfileRepository.AnyByEmail(request.Email))
        {
          _logger.LogWarning("Attempt to create duplicate user profile: {Email}", request.Email);
          return ApiResult<UserProfileDto>.Fail("Email already exists");
        }

        var entity = new UserProfile
        {
          Id = _currentUserService.UserId,
          FirstName = request.FirstName,
          LastName = request.LastName,
          Email = request.Email,
          PhoneNumber = request.PhoneNumber,
          GenderId = request.GenderId,
          BirthDate = request.BirthDate,
          NationalId = request.NationalId,
          UserType = UserType.Lawyer,
          CreatedAt = DateTime.UtcNow,
          UpdatedAt = DateTime.UtcNow
        };
        await _userProfileRepository.CreateAsync(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("CreateUserProfile completed. Id: {Id}", entity.Id);
        return ApiResult<UserProfileDto>.Success(_mapper.Map<UserProfileDto>(entity));
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error occurred while creating user profile: {Email}", request.Email);
        return ApiResult<UserProfileDto>.Fail("An unexpected error occurred");
      }
    }
  }
}


