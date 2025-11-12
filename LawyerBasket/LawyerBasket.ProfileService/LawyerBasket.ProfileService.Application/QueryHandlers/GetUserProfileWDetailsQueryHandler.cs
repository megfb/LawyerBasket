using AutoMapper;
using LawyerBasket.ProfileService.Application.Contracts.Api;
using LawyerBasket.ProfileService.Application.Contracts.Data;
using LawyerBasket.ProfileService.Application.Dtos;
using LawyerBasket.ProfileService.Application.Queries;
using LawyerBasket.Shared.Common.Response;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerBasket.ProfileService.Application.QueryHandlers
{
  public class GetUserProfileWDetailsQueryHandler : IRequestHandler<GetUserProfileWDetailsQuery, ApiResult<UserProfileWDetailsDto>>
  {
    private readonly ILogger<GetUserProfileQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IUserProfileRepository _userProfileRepository;
    private readonly ICurrentUserService _currentUserService;
    public GetUserProfileWDetailsQueryHandler(IUserProfileRepository userProfileRepository, IMapper mapper, ILogger<GetUserProfileQueryHandler> logger, ICurrentUserService currentUserService)
    {
      _userProfileRepository = userProfileRepository;
      _mapper = mapper;
      _logger = logger;
      _currentUserService = currentUserService;
    }
    public async Task<ApiResult<UserProfileWDetailsDto>> Handle(GetUserProfileWDetailsQuery request, CancellationToken cancellationToken)
    {
      _logger.LogInformation("Handling GetUserProfileQuery for UserId: {UserId}", _currentUserService.UserId);
      try
      {
        var userProfile = await _userProfileRepository.GetFullProfile(_currentUserService.UserId);
        if (userProfile is null)
        {
          _logger.LogWarning("UserProfile not found for UserId: {UserId}", _currentUserService.UserId);
          return ApiResult<UserProfileWDetailsDto>.Fail("User profile not found.");
        }

        var userProfileDto = _mapper.Map<UserProfileWDetailsDto>(userProfile);

        _logger.LogInformation("Successfully retrieved UserProfile for UserId: {UserId}", _currentUserService.UserId);
        return ApiResult<UserProfileWDetailsDto>.Success(userProfileDto);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error occurred while retrieving UserProfile for UserId: {UserId}", _currentUserService.UserId);
        return ApiResult<UserProfileWDetailsDto>.Fail("An error occurred while retrieving the user profile.");
      }
    }
  }
}
