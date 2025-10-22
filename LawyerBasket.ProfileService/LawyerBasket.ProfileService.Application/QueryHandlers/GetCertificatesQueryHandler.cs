using AutoMapper;
using LawyerBasket.ProfileService.Application.Contracts.Data;
using LawyerBasket.ProfileService.Application.Dtos;
using LawyerBasket.ProfileService.Application.Queries;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawyerBasket.ProfileService.Application.QueryHandlers
{
  public class GetCertificatesQueryHandler : IRequestHandler<GetCertificatesQuery, ApiResult<List<CertificatesDto>>>
  {
    private readonly ICertificateRepository _certificateRepository;
    private readonly ILogger<GetCertificateQueryHandler> _logger;
    private readonly IMapper _mapper;
    public GetCertificatesQueryHandler(IMapper mapper, ICertificateRepository certificateRepository, ILogger<GetCertificateQueryHandler> logger)
    {
      _certificateRepository = certificateRepository;
      _logger = logger;
      _mapper = mapper;
    }

    public async Task<ApiResult<List<CertificatesDto>>> Handle(GetCertificatesQuery request, CancellationToken cancellationToken)
    {
      _logger.LogInformation("Handling GetCertificatesQuery");
      try
      {
        var certificates = await _certificateRepository.GetAllByLawyerIdAsync(request.LawyerProfileId);
        var certificatesDto = _mapper.Map<List<CertificatesDto>>(certificates);
        _logger.LogInformation("GetCertificatesQuery handled successfully");
        return ApiResult<List<CertificatesDto>>.Success(certificatesDto);
      }
      catch (Exception)
      {
        _logger.LogError("Error occurred while handling GetCertificatesQuery");
        return ApiResult<List<CertificatesDto>>.Fail("An error occurred while processing your request");
      }
    }
  }
}
