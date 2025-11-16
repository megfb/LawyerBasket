using AutoMapper;
using LawyerBasket.ProfileService.Application.Contracts.Data;
using LawyerBasket.ProfileService.Application.Dtos;
using LawyerBasket.ProfileService.Application.Queries;
using LawyerBasket.Shared.Common.Response;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawyerBasket.ProfileService.Application.QueryHandlers
{
    public class GetUserProfilesByIdsQueryHandler : IRequestHandler<GetUserProfilesByIdsQuery, ApiResult<IEnumerable<UserProfileDto>>>
    {
        private readonly ILogger<GetUserProfilesByIdsQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IUserProfileRepository _userProfileRepository;
        
        public GetUserProfilesByIdsQueryHandler(
            IUserProfileRepository userProfileRepository, 
            IMapper mapper, 
            ILogger<GetUserProfilesByIdsQueryHandler> logger)
        {
            _userProfileRepository = userProfileRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResult<IEnumerable<UserProfileDto>>> Handle(GetUserProfilesByIdsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling GetUserProfilesByIdsQuery for {Count} UserIds", request.Ids?.Count() ?? 0);
            try
            {
                if (request.Ids == null || !request.Ids.Any())
                {
                    _logger.LogWarning("Ids collection is null or empty");
                    return ApiResult<IEnumerable<UserProfileDto>>.Fail("Ids collection cannot be null or empty.");
                }

                var userProfiles = await _userProfileRepository.GetByIdsAsync(request.Ids);
                
                if (userProfiles == null || !userProfiles.Any())
                {
                    _logger.LogWarning("No UserProfiles found for the provided Ids");
                    return ApiResult<IEnumerable<UserProfileDto>>.Success(Enumerable.Empty<UserProfileDto>());
                }

                var userProfileDtos = _mapper.Map<IEnumerable<UserProfileDto>>(userProfiles);

                _logger.LogInformation("Successfully retrieved {Count} UserProfiles", userProfileDtos.Count());
                return ApiResult<IEnumerable<UserProfileDto>>.Success(userProfileDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving UserProfiles by Ids");
                return ApiResult<IEnumerable<UserProfileDto>>.Fail("An error occurred while retrieving the user profiles.");
            }
        }
    }
}

