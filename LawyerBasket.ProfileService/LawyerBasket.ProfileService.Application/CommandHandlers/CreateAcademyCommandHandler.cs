using LawyerBasket.ProfileService.Application.Commands;
using LawyerBasket.ProfileService.Application.Contracts.Data;
using LawyerBasket.ProfileService.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawyerBasket.ProfileService.Application.CommandHandlers
{
  public class CreateAcademyCommandHandler : IRequestHandler<CreateAcademyCommand, ApiResult<string>>
  {
    private readonly IAcademyRepository _academyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateAcademyCommandHandler> _logger;
    public CreateAcademyCommandHandler(IAcademyRepository academyRepository, IUnitOfWork unitOfWork, ILogger<CreateAcademyCommandHandler> logger)
    {
      _academyRepository = academyRepository;
      _unitOfWork = unitOfWork;
      _logger = logger;
    }
    public async Task<ApiResult<string>> Handle(CreateAcademyCommand request, CancellationToken cancellationToken)
    {
      try
      {
        _logger.LogInformation("CreateAcademy started. LawyerProfileId: {LawyerProfileId}", request.LawyerProfileId);
        var entity = new Academy
        {
          Id = Guid.NewGuid().ToString(),
          LawyerProfileId = request.LawyerProfileId,
          University = request.University,
          Degree = request.Degree,
          StartDate = request.StartDate,
          EndDate = request.EndDate,
          CreatedAt = DateTime.UtcNow,
          UpdatedAt = DateTime.UtcNow
        };
        await _academyRepository.CreateAsync(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return ApiResult<string>.Success(entity.Id);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error creating academy for lawyer profile {LawyerProfileId}", request.LawyerProfileId);
        return ApiResult<string>.Fail("An unexpected error occurred");
      }
    }
  }
}


