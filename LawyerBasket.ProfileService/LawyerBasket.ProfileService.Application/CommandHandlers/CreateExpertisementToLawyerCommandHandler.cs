using LawyerBasket.ProfileService.Application.Commands;
using LawyerBasket.ProfileService.Application.Contracts.Data;
using LawyerBasket.ProfileService.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawyerBasket.ProfileService.Application.CommandHandlers
{
  public class CreateExpertisementToLawyerCommandHandler : IRequestHandler<CreateExpertisementToLawyerCommand, ApiResult<string>>
  {
    private readonly ILawyerExpertisementRepository _lawyerExpertisementRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateExpertisementToLawyerCommandHandler> _logger;
    public CreateExpertisementToLawyerCommandHandler(ILawyerExpertisementRepository lawyerExpertisementRepository, IUnitOfWork unitOfWork, ILogger<CreateExpertisementToLawyerCommandHandler> logger)
    {
      _lawyerExpertisementRepository = lawyerExpertisementRepository;
      _unitOfWork = unitOfWork;
      _logger = logger;
    }
    public async Task<ApiResult<string>> Handle(CreateExpertisementToLawyerCommand request, CancellationToken cancellationToken)
    {
      try
      {
        _logger.LogInformation("AddExpertisement started. LawyerProfileId: {LawyerProfileId} ExpertisementId: {ExpertisementId}", request.LawyerProfileId, request.ExpertisementId);
        var entity = new LawyerExpertisement
        {
          Id = Guid.NewGuid().ToString(),
          LawyerProfileId = request.LawyerProfileId,
          ExpertisementId = request.ExpertisementId,
          CreatedAt = DateTime.UtcNow,
          UpdatedAt = DateTime.UtcNow
        };
        await _lawyerExpertisementRepository.CreateAsync(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return ApiResult<string>.Success(entity.Id);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error adding expertisement to lawyer profile {LawyerProfileId}", request.LawyerProfileId);
        return ApiResult<string>.Fail("An unexpected error occurred");
      }
    }
  }
}


