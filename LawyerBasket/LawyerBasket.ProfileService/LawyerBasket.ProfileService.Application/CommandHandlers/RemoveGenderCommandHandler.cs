using LawyerBasket.ProfileService.Application.Commands;
using LawyerBasket.ProfileService.Application.Contracts.Data;
using LawyerBasket.Shared.Common.Domain;
using LawyerBasket.Shared.Common.Response;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawyerBasket.ProfileService.Application.CommandHandlers
{
    public class RemoveGenderCommandHandler : IRequestHandler<RemoveGenderCommand, ApiResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<RemoveGenderCommandHandler> _logger;
        private readonly IGenderRepository _genderRepository;
        
        public RemoveGenderCommandHandler(
            IGenderRepository genderRepository, 
            ILogger<RemoveGenderCommandHandler> logger, 
            IUnitOfWork unitOfWork)
        {
            _genderRepository = genderRepository;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        
        public async Task<ApiResult> Handle(RemoveGenderCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling RemoveGenderCommand for Id: {Id}", request.Id);
            try
            {
                var gender = await _genderRepository.GetByIdAsync(request.Id);
                if (gender == null)
                {
                    _logger.LogWarning("Gender with Id: {Id} not found", request.Id);
                    return ApiResult.Fail("Gender not found", System.Net.HttpStatusCode.NotFound);
                }
                
                _logger.LogInformation("Removing Gender with Id: {Id}", request.Id);
                _genderRepository.Delete(gender);
                
                _logger.LogInformation("Saving changes to the database for removing Gender with Id: {Id}", request.Id);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                
                _logger.LogInformation("Successfully removed Gender with Id: {Id}", request.Id);
                return ApiResult.Success(System.Net.HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while removing Gender with Id: {Id}", request.Id);
                return ApiResult.Fail("An error occurred while processing your request");
            }
        }
    }
}

