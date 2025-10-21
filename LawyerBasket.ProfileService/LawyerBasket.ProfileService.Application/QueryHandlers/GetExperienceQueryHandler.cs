using AutoMapper;
using LawyerBasket.ProfileService.Application.Contracts.Data;
using LawyerBasket.ProfileService.Application.Dtos;
using LawyerBasket.ProfileService.Application.Queries;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerBasket.ProfileService.Application.QueryHandlers
{
  public class GetExperienceQueryHandler : IRequestHandler<GetExperienceQuery, ApiResult<ExperienceDto>>
  {
    private readonly IExperienceRepository _experienceRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetExperienceQueryHandler> _logger;
    public GetExperienceQueryHandler(IExperienceRepository experienceRepository, IMapper mapper, ILogger<GetExperienceQueryHandler> logger)
    {
      _experienceRepository = experienceRepository;
      _mapper = mapper;
      _logger = logger;
    }
    public async Task<ApiResult<ExperienceDto>> Handle(GetExperienceQuery request, CancellationToken cancellationToken)
    {
      _logger.LogInformation("Handling GetExperienceQuery for Id: {Id}", request.Id);
      try
      {
        var experience = await _experienceRepository.GetByIdAsync(request.Id);
        if (experience == null)
        {
          _logger.LogWarning("Experience not found for Id: {Id}", request.Id);
          return ApiResult<ExperienceDto>.Fail("Experience not found", System.Net.HttpStatusCode.NotFound);
        }
        var experienceDto = _mapper.Map<ExperienceDto>(experience);
        _logger.LogInformation("Successfully retrieved Experience for Id: {Id}", request.Id);
        return ApiResult<ExperienceDto>.Success(experienceDto, System.Net.HttpStatusCode.OK);

      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error occurred while handling GetExperienceQuery for Id: {Id}", request.Id);
        return ApiResult<ExperienceDto>.Fail("An error occurred while processing your request.", System.Net.HttpStatusCode.InternalServerError);
      }
    }
  }
}
