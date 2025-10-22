using LawyerBasket.ProfileService.Application.Dtos;
using MediatR;

namespace LawyerBasket.ProfileService.Application.Queries
{
  public class GetCertificatesQuery : IRequest<ApiResult<List<CertificatesDto>>>
  {
    public string LawyerProfileId { get; set; }
  }
}
