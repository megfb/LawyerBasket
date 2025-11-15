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
    public class UpdateExpertisementCommandHandler : IRequestHandler<UpdateExpertisementCommand, ApiResult<ExpertisementDto>>
    {
        private readonly IExpertisemenetRepository _expertisementRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UpdateExpertisementCommandHandler> _logger;
        private readonly IMapper _mapper;
        
        public UpdateExpertisementCommandHandler(
            IExpertisemenetRepository expertisementRepository, 
            IUnitOfWork unitOfWork, 
            ILogger<UpdateExpertisementCommandHandler> logger, 
            IMapper mapper)
        {
            _expertisementRepository = expertisementRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        
        public async Task<ApiResult<ExpertisementDto>> Handle(UpdateExpertisementCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("UpdateExpertisementCommandHandler Handle method invoked for Id: {Id}", request.Id);
            try
            {
                var expertisement = await _expertisementRepository.GetByIdAsync(request.Id);
                if (expertisement == null)
                {
                    _logger.LogWarning("Expertisement with Id: {ExpertisementId} not found.", request.Id);
                    return ApiResult<ExpertisementDto>.Fail("Expertisement not found", System.Net.HttpStatusCode.NotFound);
                }
                
                expertisement.Name = request.Name;
                expertisement.Description = request.Description;
                expertisement.UpdatedAt = request.UpdatedAt;
                
                _logger.LogInformation("Updating expertisement with Id: {ExpertisementId}.", request.Id);
                _expertisementRepository.Update(expertisement);
                
                _logger.LogInformation("Saving changes to the database for ExpertisementId: {ExpertisementId}.", request.Id);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                
                _logger.LogInformation("Expertisement with Id: {ExpertisementId} updated successfully.", request.Id);
                return ApiResult<ExpertisementDto>.Success(_mapper.Map<ExpertisementDto>(expertisement));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating expertisement with Id: {ExpertisementId}.", request.Id);
                return ApiResult<ExpertisementDto>.Fail("An unexpected error occurred");
            }
        }
    }
}

