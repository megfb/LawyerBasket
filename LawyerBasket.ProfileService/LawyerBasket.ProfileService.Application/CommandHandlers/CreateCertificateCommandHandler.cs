using LawyerBasket.ProfileService.Application.Commands;
using LawyerBasket.ProfileService.Application.Contracts.Data;
using LawyerBasket.ProfileService.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawyerBasket.ProfileService.Application.CommandHandlers
{
  public class CreateCertificateCommandHandler : IRequestHandler<CreateCertificateCommand, ApiResult<string>>
  {
    private readonly ICertificateRepository _certificateRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateCertificateCommandHandler> _logger;
    public CreateCertificateCommandHandler(ICertificateRepository certificateRepository, IUnitOfWork unitOfWork, ILogger<CreateCertificateCommandHandler> logger)
    {
      _certificateRepository = certificateRepository;
      _unitOfWork = unitOfWork;
      _logger = logger;
    }
    public async Task<ApiResult<string>> Handle(CreateCertificateCommand request, CancellationToken cancellationToken)
    {
      try
      {
        _logger.LogInformation("CreateCertificate started. LawyerProfileId: {LawyerProfileId}", request.LawyerProfileId);
        var entity = new Certificates
        {
          Id = Guid.NewGuid().ToString(),
          LawyerProfileId = request.LawyerProfileId,
          Name = request.Name,
          Institution = request.Institution,
          DateReceived = request.DateReceived,
          CreatedAt = DateTime.UtcNow,
          UpdatedAt = DateTime.UtcNow
        };
        await _certificateRepository.CreateAsync(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return ApiResult<string>.Success(entity.Id);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error creating certificate for lawyer profile {LawyerProfileId}", request.LawyerProfileId);
        return ApiResult<string>.Fail("An unexpected error occurred");
      }
    }
  }
}


