using LawyerBasket.ProfileService.Application.Commands;
using LawyerBasket.ProfileService.Application.Contracts.Data;
using LawyerBasket.Shared.Common.Domain;
using LawyerBasket.Shared.Common.Response;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawyerBasket.ProfileService.Application.CommandHandlers
{
    public class RemoveExpertisementCommandHandler : IRequestHandler<RemoveExpertisementCommand, ApiResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<RemoveExpertisementCommandHandler> _logger;
        private readonly IExpertisemenetRepository _expertisementRepository;
        
        public RemoveExpertisementCommandHandler(
            IExpertisemenetRepository expertisementRepository, 
            ILogger<RemoveExpertisementCommandHandler> logger, 
            IUnitOfWork unitOfWork)
        {
            _expertisementRepository = expertisementRepository;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        
        public async Task<ApiResult> Handle(RemoveExpertisementCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling RemoveExpertisementCommand for Id: {Id}", request.Id);
            try
            {
                var expertisement = await _expertisementRepository.GetByIdAsync(request.Id);
                if (expertisement == null)
                {
                    _logger.LogWarning("Expertisement with Id: {Id} not found", request.Id);
                    return ApiResult.Fail("Expertisement not found", System.Net.HttpStatusCode.NotFound);
                }
                
                _logger.LogInformation("Removing Expertisement with Id: {Id}", request.Id);
                _expertisementRepository.Delete(expertisement);
                
                _logger.LogInformation("Saving changes to the database for removing Expertisement with Id: {Id}", request.Id);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                
                _logger.LogInformation("Successfully removed Expertisement with Id: {Id}", request.Id);
                return ApiResult.Success(System.Net.HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while removing Expertisement with Id: {Id}", request.Id);
                return ApiResult.Fail("An error occurred while processing your request");
            }
        }
    }
}

