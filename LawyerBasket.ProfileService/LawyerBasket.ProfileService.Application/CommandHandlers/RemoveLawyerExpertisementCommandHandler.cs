using LawyerBasket.ProfileService.Application.Commands;
using LawyerBasket.ProfileService.Application.Contracts.Data;
using LawyerBasket.Shared.Common.Domain;
using LawyerBasket.Shared.Common.Response;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawyerBasket.ProfileService.Application.CommandHandlers
{
    public class RemoveLawyerExpertisementCommandHandler : IRequestHandler<RemoveLawyerExpertisementCommand, ApiResult>
    {
        private readonly ILawyerExpertisementRepository _lawyerExpertisementRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<RemoveLawyerExpertisementCommandHandler> _logger;
        public RemoveLawyerExpertisementCommandHandler(ILawyerExpertisementRepository lawyerExpertisementRepository, IUnitOfWork unitOfWork, ILogger<RemoveLawyerExpertisementCommandHandler> logger)
        {
            _lawyerExpertisementRepository = lawyerExpertisementRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<ApiResult> Handle(RemoveLawyerExpertisementCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("RemoveLawyerExpertisementCommand Handling");
            try
            {
                var entity = await _lawyerExpertisementRepository.GetByIdAsync(request.Id);
                if (entity is null)
                {
                    _logger.LogWarning($"LawyerExpertisement with Id {request.Id} not found.");
                    return ApiResult.Fail("LawyerExpertisement not found.");
                }

                _logger.LogInformation($"Removing LawyerExpertisement with Id {request.Id}.");
                _lawyerExpertisementRepository.Delete(entity);

                _logger.LogInformation($"Saving changes to the database for LawyerExpertisement Id {request.Id}.");
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                _logger.LogInformation($"Successfully removed LawyerExpertisement with Id {request.Id}.");
                return ApiResult.Success(System.Net.HttpStatusCode.NoContent);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while removing LawyerExpertisement with Id {request.Id}.");
                return ApiResult.Fail("An error occurred while processing your request.");
            }
        }
    }
}
