using LawyerBasket.ProfileService.Application.Commands;
using LawyerBasket.ProfileService.Application.Contracts.Data;
using LawyerBasket.ProfileService.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawyerBasket.ProfileService.Application.CommandHandlers
{
  public class CreateExperienceCommandHandler : IRequestHandler<CreateExperienceCommand, ApiResult<string>>
  {
    private readonly IExperienceRepository _experienceRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateExperienceCommandHandler> _logger;
    public CreateExperienceCommandHandler(IExperienceRepository experienceRepository, IUnitOfWork unitOfWork, ILogger<CreateExperienceCommandHandler> logger)
    {
      _experienceRepository = experienceRepository;
      _unitOfWork = unitOfWork;
      _logger = logger;
    }
    public async Task<ApiResult<string>> Handle(CreateExperienceCommand request, CancellationToken cancellationToken)
    {
      try
      {
        _logger.LogInformation("CreateExperience started. LawyerProfileId: {LawyerProfileId}", request.LawyerProfileId);
        var entity = new Experience
        {
          Id = Guid.NewGuid().ToString(),
          LawyerProfileId = request.LawyerProfileId,
          CompanyName = request.CompanyName,
          Position = request.Position,
          StartDate = request.StartDate,
          EndDate = request.EndDate,
          Description = request.Description,
          CreatedAt = DateTime.UtcNow,
          UpdatedAt = DateTime.UtcNow
        };
        await _experienceRepository.CreateAsync(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return ApiResult<string>.Success(entity.Id);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error creating experience for lawyer profile {LawyerProfileId}", request.LawyerProfileId);
        return ApiResult<string>.Fail("An unexpected error occurred");
      }
    }
  }
}


