using AutoMapper;
using LawyerBasket.ProfileService.Application.Commands;
using LawyerBasket.ProfileService.Application.Contracts.Data;
using LawyerBasket.ProfileService.Application.Dtos;
using LawyerBasket.Shared.Common.Domain;
using LawyerBasket.Shared.Common.Response;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawyerBasket.ProfileService.Application.CommandHandlers
{
    public class CreateLawyerExpertisementCommandHandler : IRequestHandler<CreateLawyerExpertisementCommand, ApiResult<List<LawyerExpertisementDto>>>
    {
        private readonly ILawyerExpertisementRepository _lawyerExpertisementRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfwork;
        private readonly ILogger<CreateLawyerExpertisementCommandHandler> _logger;
        public CreateLawyerExpertisementCommandHandler(ILogger<CreateLawyerExpertisementCommandHandler> logger, IUnitOfWork unitOfwork, IMapper mapper, ILawyerExpertisementRepository lawyerExpertisementRepository)
        {
            _logger = logger;
            _unitOfwork = unitOfwork;
            _mapper = mapper;
            _lawyerExpertisementRepository = lawyerExpertisementRepository;
        }
        public async Task<ApiResult<List<LawyerExpertisementDto>>> Handle(CreateLawyerExpertisementCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling CreateLawyerExpertisementCommand for LawyerProfileId: {LawyerProfileId} with {Count} expertisements", 
                request.LawyerProfileId, request.ExpertisementIds?.Count ?? 0);
            try
            {
                if (request.ExpertisementIds == null || !request.ExpertisementIds.Any())
                {
                    _logger.LogWarning("No expertisement IDs provided for LawyerProfileId: {LawyerProfileId}", request.LawyerProfileId);
                    return ApiResult<List<LawyerExpertisementDto>>.Fail("At least one expertisement ID must be provided");
                }

                var entities = request.ExpertisementIds.Select(expertisementId => new Domain.Entities.LawyerExpertisement
                {
                    Id = Guid.NewGuid().ToString(),
                    LawyerProfileId = request.LawyerProfileId,
                    ExpertisementId = expertisementId,
                    CreatedAt = DateTime.UtcNow,
                }).ToList();

                _logger.LogInformation("Creating {Count} LawyerExpertisement entities for LawyerProfileId: {LawyerProfileId}", 
                    entities.Count, request.LawyerProfileId);
                
                await _lawyerExpertisementRepository.CreateRangeAsync(entities);
                
                _logger.LogInformation("Saving changes to the database for LawyerProfileId: {LawyerProfileId}", request.LawyerProfileId);
                await _unitOfwork.SaveChangesAsync(cancellationToken);
                
                _logger.LogInformation("Successfully created {Count} LawyerExpertisement entities for LawyerProfileId: {LawyerProfileId}", 
                    entities.Count, request.LawyerProfileId);
                
                var dtos = _mapper.Map<List<LawyerExpertisementDto>>(entities);
                return ApiResult<List<LawyerExpertisementDto>>.Success(dtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating LawyerExpertisements for LawyerProfileId: {LawyerProfileId}", request.LawyerProfileId);
                return ApiResult<List<LawyerExpertisementDto>>.Fail("An unexpected error occurred");
            }
        }
    }
}
