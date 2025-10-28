using AutoMapper;
using LawyerBasket.ProfileService.Application.Contracts.Data;
using LawyerBasket.ProfileService.Application.Dtos;
using LawyerBasket.ProfileService.Application.Queries;
using LawyerBasket.Shared.Common.Response;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawyerBasket.ProfileService.Application.QueryHandlers
{
  public class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQuery, ApiResult<UserProfileDto>>
  {
    private readonly ILogger<GetUserProfileQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IUserProfileRepository _userProfileRepository;
    public GetUserProfileQueryHandler(IUserProfileRepository userProfileRepository, IMapper mapper, ILogger<GetUserProfileQueryHandler> logger)
    {
      _userProfileRepository = userProfileRepository;
      _mapper = mapper;
      _logger = logger;
    }
    public async Task<ApiResult<UserProfileDto>> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
    {
      _logger.LogInformation("Handling GetUserProfileQuery for UserId: {UserId}", request.Id);
      try
      {
        var userProfile = await _userProfileRepository.GetByIdAsync(request.Id);
        if (userProfile is null)
        {
          _logger.LogWarning("UserProfile not found for UserId: {UserId}", request.Id);
          return ApiResult<UserProfileDto>.Fail("User profile not found.");
        }

        var userProfileDto = _mapper.Map<UserProfileDto>(userProfile);

        _logger.LogInformation("Successfully retrieved UserProfile for UserId: {UserId}", request.Id);
        return ApiResult<UserProfileDto>.Success(userProfileDto);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error occurred while retrieving UserProfile for UserId: {UserId}", request.Id);
        return ApiResult<UserProfileDto>.Fail("An error occurred while retrieving the user profile.");
      }
    }
  }
}
