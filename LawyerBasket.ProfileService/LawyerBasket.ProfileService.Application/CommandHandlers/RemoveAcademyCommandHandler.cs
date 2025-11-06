using LawyerBasket.ProfileService.Application.Commands;
using LawyerBasket.ProfileService.Application.Contracts.Data;
using LawyerBasket.Shared.Common.Domain;
using LawyerBasket.Shared.Common.Response;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawyerBasket.ProfileService.Application.CommandHandlers
{
    public class RemoveAcademyCommandHandler : IRequestHandler<RemoveAcademyCommand, ApiResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<RemoveAcademyCommandHandler> _logger;
        private readonly IAcademyRepository _academyRepository;
        public RemoveAcademyCommandHandler(IAcademyRepository academyRepository, ILogger<RemoveAcademyCommandHandler> logger, IUnitOfWork unitOfWork)
        {
            _academyRepository = academyRepository;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiResult> Handle(RemoveAcademyCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling RemoveAcademyCommand for Id: {Id}", request.Id);
            try
            {
                var academy = await _academyRepository.GetByIdAsync(request.Id);
                if (academy == null)
                {
                    _logger.LogWarning("Academy with Id: {Id} not found", request.Id);
                    return ApiResult.Fail("Academy not found", System.Net.HttpStatusCode.NotFound);
                }
                _logger.LogInformation("Removing Academy with Id: {Id}", request.Id);
                _academyRepository.Delete(academy);
                _logger.LogInformation("Saving changes to the database for removing Academy with Id: {Id}", request.Id);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("Successfully removed Academy with Id: {Id}", request.Id);
                return ApiResult.Success(System.Net.HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while removing Academy with Id: {Id}", request.Id);
                return ApiResult.Fail("An error occurred while processing your request");
            }
        }
    }
}
