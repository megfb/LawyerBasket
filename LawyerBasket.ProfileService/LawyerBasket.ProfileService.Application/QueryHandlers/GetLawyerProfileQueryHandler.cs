using AutoMapper;
using LawyerBasket.ProfileService.Application.Contracts.Data;
using LawyerBasket.ProfileService.Application.Dtos;
using LawyerBasket.ProfileService.Application.Queries;
using LawyerBasket.Shared.Common.Response;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawyerBasket.ProfileService.Application.QueryHandlers
{
  public class GetLawyerProfileQueryHandler : IRequestHandler<GetLawyerProfileQuery, ApiResult<LawyerProfileDto>>
  {
    private readonly IMapper _mapper;
    private readonly ILogger<GetLawyerProfileQueryHandler> _logger;
    private readonly ILawyerProfileRepository _lawyerProfileRepository;
    public GetLawyerProfileQueryHandler(IMapper mapper, ILogger<GetLawyerProfileQueryHandler> logger, ILawyerProfileRepository lawyerProfileRepository)
    {
      _lawyerProfileRepository = lawyerProfileRepository;
      _logger = logger;
      _mapper = mapper;
    }
    public async Task<ApiResult<LawyerProfileDto>> Handle(GetLawyerProfileQuery request, CancellationToken cancellationToken)
    {

      _logger.LogInformation("Handling GetLawyerProfileQuery for LawyerId: {Id}", request.Id);

      try
      {
        var lawyer = await _lawyerProfileRepository.GetByIdAsync(request.Id);
        if (lawyer is null)
        {
          _logger.LogError("Lawyer profile not found for Id: {Id}", request.Id);
          return ApiResult<LawyerProfileDto>.Fail("Lawyer profile not found");
        }
        var lawyerDto = _mapper.Map<LawyerProfileDto>(lawyer);

        _logger.LogInformation("Successfully retrieved lawyer profile for Id: {Id}", request.Id);
        return ApiResult<LawyerProfileDto>.Success(lawyerDto);
      }
      catch (Exception)
      {
        _logger.LogError("An error occurred while retrieving lawyer profile for Id: {Id}", request.Id);
        return ApiResult<LawyerProfileDto>.Fail("An error occurred while processing your request");
      }
    }
  }
}
