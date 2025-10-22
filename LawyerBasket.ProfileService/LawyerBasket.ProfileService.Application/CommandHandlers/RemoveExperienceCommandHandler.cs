using LawyerBasket.ProfileService.Application.Commands;
using LawyerBasket.ProfileService.Application.Contracts.Data;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawyerBasket.ProfileService.Application.CommandHandlers
{
  public class RemoveExperienceCommandHandler : IRequestHandler<RemoveExperienceCommand, ApiResult>
  {
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<RemoveExperienceCommandHandler> _logger;
    private readonly IExperienceRepository _experienceRepository;
    public RemoveExperienceCommandHandler(IExperienceRepository experienceRepository, ILogger<RemoveExperienceCommandHandler> logger, IUnitOfWork unitOfWork)
    {
      _experienceRepository = experienceRepository;
      _logger = logger;
      _unitOfWork = unitOfWork;
    }
    public async Task<ApiResult> Handle(RemoveExperienceCommand request, CancellationToken cancellationToken)
    {
      _logger.LogInformation("RemoveExperienceCommandHandler called with Id: {Id}", request.Id);
      try
      {
        var experience = await _experienceRepository.GetByIdAsync(request.Id);
        if (experience == null)
        {
          _logger.LogWarning("Experience with Id: {Id} not found", request.Id);
          return ApiResult.Fail("Experience not found", System.Net.HttpStatusCode.NotFound);
        }
        _logger.LogInformation("Removing Experience with Id: {Id}", request.Id);
        _experienceRepository.Delete(experience);
        _logger.LogInformation("Saving changes to the database for Experience Id: {Id}", request.Id);
        await _unitOfWork.SaveChangesAsync();
        _logger.LogInformation("Experience with Id: {Id} removed successfully", request.Id);
        return ApiResult.Success(System.Net.HttpStatusCode.OK);

      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error occurred while removing Experience with Id: {Id}", request.Id);
        return ApiResult.Fail("An error occurred while removing the experience.", System.Net.HttpStatusCode.InternalServerError);
      }
    }
  }
}
