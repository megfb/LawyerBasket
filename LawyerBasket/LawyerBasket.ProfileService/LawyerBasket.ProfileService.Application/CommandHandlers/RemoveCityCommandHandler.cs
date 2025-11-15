using LawyerBasket.ProfileService.Application.Commands;
using LawyerBasket.ProfileService.Application.Contracts.Data;
using LawyerBasket.Shared.Common.Domain;
using LawyerBasket.Shared.Common.Response;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawyerBasket.ProfileService.Application.CommandHandlers
{
    public class RemoveCityCommandHandler : IRequestHandler<RemoveCityCommand, ApiResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<RemoveCityCommandHandler> _logger;
        private readonly ICityRepository _cityRepository;
        
        public RemoveCityCommandHandler(
            ICityRepository cityRepository, 
            ILogger<RemoveCityCommandHandler> logger, 
            IUnitOfWork unitOfWork)
        {
            _cityRepository = cityRepository;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        
        public async Task<ApiResult> Handle(RemoveCityCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling RemoveCityCommand for Id: {Id}", request.Id);
            try
            {
                var city = await _cityRepository.GetByIdAsync(request.Id);
                if (city == null)
                {
                    _logger.LogWarning("City with Id: {Id} not found", request.Id);
                    return ApiResult.Fail("City not found", System.Net.HttpStatusCode.NotFound);
                }
                
                _logger.LogInformation("Removing City with Id: {Id}", request.Id);
                _cityRepository.Delete(city);
                
                _logger.LogInformation("Saving changes to the database for removing City with Id: {Id}", request.Id);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                
                _logger.LogInformation("Successfully removed City with Id: {Id}", request.Id);
                return ApiResult.Success(System.Net.HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while removing City with Id: {Id}", request.Id);
                return ApiResult.Fail("An error occurred while processing your request");
            }
        }
    }
}

