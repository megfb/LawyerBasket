using AutoMapper;
using LawyerBasket.ProfileService.Application.Contracts.Data;
using LawyerBasket.ProfileService.Application.Dtos;
using LawyerBasket.ProfileService.Application.Queries;
using LawyerBasket.Shared.Common.Response;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawyerBasket.ProfileService.Application.QueryHandlers
{
    public class GetExpertisementsQueryHandler : IRequestHandler<GetExpertisementsQuery, ApiResult<List<ExpertisementDto>>>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<GetExpertisementsQueryHandler> _logger;
        private readonly IExpertisemenetRepository _expertisementRepository;
        
        public GetExpertisementsQueryHandler(
            IExpertisemenetRepository expertisementRepository, 
            ILogger<GetExpertisementsQueryHandler> logger, 
            IMapper mapper)
        {
            _expertisementRepository = expertisementRepository;
            _logger = logger;
            _mapper = mapper;
        }
        
        public async Task<ApiResult<List<ExpertisementDto>>> Handle(GetExpertisementsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling GetExpertisementsQuery");
            try
            {
                var expertisements = await _expertisementRepository.GetAllAsync();
                var expertisementDtos = _mapper.Map<List<ExpertisementDto>>(expertisements);
                _logger.LogInformation("Successfully retrieved {Count} expertisements", expertisementDtos.Count);
                return ApiResult<List<ExpertisementDto>>.Success(expertisementDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving expertisements");
                return ApiResult<List<ExpertisementDto>>.Fail("An error occurred while processing your request");
            }
        }
    }
}

