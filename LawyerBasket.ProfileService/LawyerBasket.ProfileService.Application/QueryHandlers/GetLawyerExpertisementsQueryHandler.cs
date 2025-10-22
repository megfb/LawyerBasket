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
  public class GetLawyerExpertisementsQueryHandler : IRequestHandler<GetLawyerExpertisementsQuery, ApiResult<List<LawyerExpertisementDto>>>
  {
    private readonly ILogger<GetLawyerExpertisementsQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly ILawyerExpertisementRepository _lawyerExpertisementsRepository;
    public GetLawyerExpertisementsQueryHandler(ILogger<GetLawyerExpertisementsQueryHandler> logger, IMapper mapper, ILawyerExpertisementRepository lawyerExpertisementRepository)
    {
      _lawyerExpertisementsRepository = lawyerExpertisementRepository;
      _mapper = mapper;
      _logger = logger;
    }
    public async Task<ApiResult<List<LawyerExpertisementDto>>> Handle(GetLawyerExpertisementsQuery request, CancellationToken cancellationToken)
    {
      _logger.LogInformation("Handling GetLawyerExpertisementsQuery for LawyerProfileId: {LawyerProfileId}", request.LawyerProfileId);
      try
      {
        var expertisements = await _lawyerExpertisementsRepository.GetAllByLawyerProfileIdAsync(request.LawyerProfileId);
        var expertisementDtos = _mapper.Map<List<LawyerExpertisementDto>>(expertisements);
        _logger.LogInformation("Retrieved {Count} expertisements for LawyerProfileId: {LawyerProfileId}", expertisementDtos.Count, request.LawyerProfileId);
        return ApiResult<List<LawyerExpertisementDto>>.Success(expertisementDtos);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error occurred while handling GetLawyerExpertisementQuery");
        return ApiResult<List<LawyerExpertisementDto>>.Fail("An error occurred while processing your request.");
      }
    }
  }
}
