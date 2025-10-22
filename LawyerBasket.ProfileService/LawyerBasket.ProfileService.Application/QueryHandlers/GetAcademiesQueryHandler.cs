using AutoMapper;
using LawyerBasket.ProfileService.Application.Contracts.Data;
using LawyerBasket.ProfileService.Application.Dtos;
using LawyerBasket.ProfileService.Application.Queries;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawyerBasket.ProfileService.Application.QueryHandlers
{
  public class GetAcademiesQueryHandler : IRequestHandler<GetAcademiesQuery, ApiResult<List<AcademyDto>>>
  {
    private readonly IMapper _mapper;
    private readonly ILogger<GetAcademiesQueryHandler> _logger;
    private readonly IAcademyRepository _academyRepository;
    public GetAcademiesQueryHandler(IAcademyRepository academyRepository, ILogger<GetAcademiesQueryHandler> logger, IMapper mapper)
    {
      _academyRepository = academyRepository;
      _logger = logger;
      _mapper = mapper;
    }
    public async Task<ApiResult<List<AcademyDto>>> Handle(GetAcademiesQuery request, CancellationToken cancellationToken)
    {
      _logger.LogInformation("Handling GetAcademiesQuery");
      try
      {
        var academies = await _academyRepository.GetAllByLawyerIdAsync(request.Id);
        var academyDtos = _mapper.Map<List<AcademyDto>>(academies);
        _logger.LogInformation("Successfully retrieved {Count} academies", academyDtos.Count);
        return ApiResult<List<AcademyDto>>.Success(academyDtos);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error occurred while retrieving academies");
        return ApiResult<List<AcademyDto>>.Fail("An error occurred while processing your request");
      }
    }
  }
}
