using AutoMapper;
using LawyerBasket.ProfileService.Application.Commands;
using LawyerBasket.ProfileService.Application.Contracts.Data;
using LawyerBasket.ProfileService.Application.Dtos;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawyerBasket.ProfileService.Application.CommandHandlers
{
  public class UpdateCertificateCommandHandler : IRequestHandler<UpdateCertificateCommand, ApiResult<CertificatesDto>>
  {
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UpdateCertificateCommandHandler> _logger;
    private readonly ICertificateRepository _certificateRepository;
    public UpdateCertificateCommandHandler(ICertificateRepository certificateRepository, ILogger<UpdateCertificateCommandHandler> logger, IMapper mapper, IUnitOfWork unitOfWork)
    {
      _certificateRepository = certificateRepository;
      _logger = logger;
      _mapper = mapper;
      _unitOfWork = unitOfWork;
    }
    public async Task<ApiResult<CertificatesDto>> Handle(UpdateCertificateCommand request, CancellationToken cancellationToken)
    {
      _logger.LogInformation("Handling UpdateCertificateCommand for Certificate Id: {CertificateId}", request.Id);
      try
      {
        var certificateEntity = await _certificateRepository.GetByIdAsync(request.Id);
        if (certificateEntity == null)
        {
          _logger.LogWarning("Certificate with Id: {CertificateId} not found", request.Id);
          return ApiResult<CertificatesDto>.Fail("Certificate not found");
        }
        certificateEntity.Name = request.Name;
        certificateEntity.Institution = request.Institution;
        certificateEntity.DateReceived = request.DateReceived;
        certificateEntity.UpdatedAt = DateTime.UtcNow;

        _logger.LogInformation("Updating Certificate with Id: {CertificateId}", request.Id);
        _certificateRepository.Update(certificateEntity);

        _logger.LogInformation("Saving changes to the database for Certificate Id: {CertificateId}", request.Id);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var certificateDto = _mapper.Map<CertificatesDto>(certificateEntity);
        _logger.LogInformation("Successfully updated Certificate with Id: {CertificateId}", request.Id);
        return ApiResult<CertificatesDto>.Success(certificateDto);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error occurred while updating Certificate with Id: {CertificateId}", request.Id);
        return ApiResult<CertificatesDto>.Fail("An error occurred while updating the certificate.");
      }
    }
  }
}
