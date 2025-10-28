using AutoMapper;
using LawyerBasket.ProfileService.Application.Contracts.Data;
using LawyerBasket.ProfileService.Application.Dtos;
using LawyerBasket.ProfileService.Application.Queries;
using LawyerBasket.Shared.Common.Response;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawyerBasket.ProfileService.Application.QueryHandlers
{
  public class GetCertificateQueryHandler : IRequestHandler<GetCertificateQuery, ApiResult<CertificatesDto>>
  {
    private readonly IMapper _mapper;
    private readonly ICertificateRepository _certificateRepository;
    private readonly ILogger<GetCertificateQueryHandler> _logger;
    public GetCertificateQueryHandler(ILogger<GetCertificateQueryHandler> logger, IMapper mapper, ICertificateRepository certificateRepository)
    {
      _mapper = mapper;
      _certificateRepository = certificateRepository;
      _logger = logger;
    }
    public async Task<ApiResult<CertificatesDto>> Handle(GetCertificateQuery request, CancellationToken cancellationToken)
    {
      _logger.LogInformation("Handling GetCertificateQuery for Certificate Id: {CertificateId}", request.Id);
      try
      {
        var certificate = await _certificateRepository.GetByIdAsync(request.Id);
        if (certificate == null)
        {
          _logger.LogWarning("Certificate with Id: {CertificateId} not found", request.Id);
          return ApiResult<CertificatesDto>.Fail("Certificate not found");
        }
        var certificateDto = _mapper.Map<CertificatesDto>(certificate);
        _logger.LogInformation("Successfully retrieved Certificate with Id: {CertificateId}", request.Id);
        return ApiResult<CertificatesDto>.Success(certificateDto);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error occurred while retrieving Certificate with Id: {CertificateId}", request.Id);
        return ApiResult<CertificatesDto>.Fail("An error occurred while retrieving the certificate.");
      }
    }
  }
}
