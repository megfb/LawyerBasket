using AutoMapper;
using LawyerBasket.ProfileService.Application.Commands;
using LawyerBasket.ProfileService.Application.Contracts.Data;
using LawyerBasket.ProfileService.Application.Dtos;
using LawyerBasket.ProfileService.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawyerBasket.ProfileService.Application.CommandHandlers
{
  public class CreateCertificateCommandHandler : IRequestHandler<CreateCertificateCommand, ApiResult<CertificatesDto>>
  {
    private readonly ICertificateRepository _certificateRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateCertificateCommandHandler> _logger;
    private readonly IMapper _mapper;
    public CreateCertificateCommandHandler(IMapper mapper, ICertificateRepository certificateRepository, IUnitOfWork unitOfWork, ILogger<CreateCertificateCommandHandler> logger)
    {
      _certificateRepository = certificateRepository;
      _unitOfWork = unitOfWork;
      _logger = logger;
      _mapper = mapper;
    }
    public async Task<ApiResult<CertificatesDto>> Handle(CreateCertificateCommand request, CancellationToken cancellationToken)
    {
      _logger.LogInformation("CreateCertificate started. LawyerProfileId: {LawyerProfileId}", request.LawyerProfileId);
      try
      {
        var entity = new Certificates
        {
          Id = Guid.NewGuid().ToString(),
          LawyerProfileId = request.LawyerProfileId,
          Name = request.Name,
          Institution = request.Institution,
          DateReceived = request.DateReceived,
          CreatedAt = DateTime.UtcNow,
        };
        _logger.LogInformation("Creating certificate entity for LawyerProfileId: {LawyerProfileId}", request.LawyerProfileId);
        await _certificateRepository.CreateAsync(entity);
        _logger.LogInformation("Saving changes to the database for LawyerProfileId: {LawyerProfileId}", request.LawyerProfileId);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return ApiResult<CertificatesDto>.Success(_mapper.Map<CertificatesDto>(entity));
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error creating certificate for lawyer profile {LawyerProfileId}", request.LawyerProfileId);
        return ApiResult<CertificatesDto>.Fail("An unexpected error occurred");
      }
    }
  }
}


