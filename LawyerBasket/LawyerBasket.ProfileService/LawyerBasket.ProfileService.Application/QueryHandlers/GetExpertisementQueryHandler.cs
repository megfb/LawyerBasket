using AutoMapper;
using LawyerBasket.ProfileService.Application.Contracts.Data;
using LawyerBasket.ProfileService.Application.Dtos;
using LawyerBasket.ProfileService.Application.Queries;
using LawyerBasket.Shared.Common.Response;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawyerBasket.ProfileService.Application.QueryHandlers
{
    public class GetExpertisementQueryHandler : IRequestHandler<GetExpertisementQuery, ApiResult<ExpertisementDto>>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<GetExpertisementQueryHandler> _logger;
        private readonly IExpertisemenetRepository _expertisementRepository;
        
        public GetExpertisementQueryHandler(
            IExpertisemenetRepository expertisementRepository, 
            IMapper mapper, 
            ILogger<GetExpertisementQueryHandler> logger)
        {
            _expertisementRepository = expertisementRepository;
            _mapper = mapper;
            _logger = logger;
        }
        
        public async Task<ApiResult<ExpertisementDto>> Handle(GetExpertisementQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling GetExpertisementQuery for Id: {Id}", request.Id);
            try
            {
                var expertisement = await _expertisementRepository.GetByIdAsync(request.Id);
                if (expertisement == null)
                {
                    _logger.LogWarning("Expertisement with Id: {Id} not found", request.Id);
                    return ApiResult<ExpertisementDto>.Fail($"Expertisement with Id: {request.Id} not found", System.Net.HttpStatusCode.NotFound);
                }
                
                var expertisementDto = _mapper.Map<ExpertisementDto>(expertisement);
                _logger.LogInformation("Successfully retrieved Expertisement with Id: {Id}", request.Id);
                return ApiResult<ExpertisementDto>.Success(expertisementDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while handling GetExpertisementQuery for Id: {Id}", request.Id);
                return ApiResult<ExpertisementDto>.Fail("An error occurred while processing your request.", System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}

