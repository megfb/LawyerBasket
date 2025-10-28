using AutoMapper;
using LawyerBasket.ProfileService.Application.Contracts.Data;
using LawyerBasket.ProfileService.Application.Dtos;
using LawyerBasket.ProfileService.Application.Queries;
using LawyerBasket.Shared.Common.Response;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawyerBasket.ProfileService.Application.QueryHandlers
{
  public class GetExperiencesQueryHandler : IRequestHandler<GetExperiencesQuery, ApiResult<List<ExperienceDto>>>
  {
    private readonly IExperienceRepository _experienceRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetExperiencesQueryHandler> _logger;
    public GetExperiencesQueryHandler(IExperienceRepository experienceRepository, ILogger<GetExperiencesQueryHandler> logger, IMapper mapper)
    {
      _experienceRepository = experienceRepository;
      _logger = logger;
      _mapper = mapper;
    }
    public async Task<ApiResult<List<ExperienceDto>>> Handle(GetExperiencesQuery request, CancellationToken cancellationToken)
    {
      _logger.LogInformation("Handling GetExperiencesQuery");
      try
      {
        var experiences = await _experienceRepository.GetAllByLawyerIdAsync(request.Id);
        var experienceDtos = _mapper.Map<List<ExperienceDto>>(experiences);

        _logger.LogInformation("Successfully retrieved {Count} experiences", experienceDtos.Count);
        return ApiResult<List<ExperienceDto>>.Success(experienceDtos);

      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error occurred while handling GetExperiencesQuery");
        return ApiResult<List<ExperienceDto>>.Fail("An error occurred while processing your request.");
      }
    }
  }
}
