using LawyerBasket.ProfileService.Application.Commands;
using LawyerBasket.ProfileService.Application.Contracts.Data;
using LawyerBasket.ProfileService.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawyerBasket.ProfileService.Application.CommandHandlers
{
  public class CreateLawyerProfileCommandHandler : IRequestHandler<CreateLawyerProfileCommand, ApiResult<string>>
  {
    private readonly ILawyerProfileRepository _lawyerProfileRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateLawyerProfileCommandHandler> _logger;
    public CreateLawyerProfileCommandHandler(ILawyerProfileRepository lawyerProfileRepository, IUnitOfWork unitOfWork, ILogger<CreateLawyerProfileCommandHandler> logger)
    {
      _lawyerProfileRepository = lawyerProfileRepository;
      _unitOfWork = unitOfWork;
      _logger = logger;
    }
    public async Task<ApiResult<string>> Handle(CreateLawyerProfileCommand request, CancellationToken cancellationToken)
    {
      try
      {
        _logger.LogInformation("CreateLawyerProfile started. UserProfileId: {UserProfileId}", request.UserProfileId);
        var entity = new LawyerProfile
        {
          Id = Guid.NewGuid().ToString(),
          UserProfileId = request.UserProfileId,
          BarAssociation = request.BarAssociation,
          BarNumber = request.BarNumber,
          LicenseNumber = request.LicenseNumber,
          LicenseDate = request.LicenseDate,
          CreatedAt = DateTime.UtcNow,
          UpdatedAt = DateTime.UtcNow
        };
        await _lawyerProfileRepository.CreateAsync(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return ApiResult<string>.Success(entity.Id);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error creating lawyer profile for user {UserProfileId}", request.UserProfileId);
        return ApiResult<string>.Fail("An unexpected error occurred");
      }
    }
  }
}


