using LawyerBasket.ProfileService.Application.Dtos;
using MediatR;

namespace LawyerBasket.ProfileService.Application.Queries
{
  public class GetCertificateQuery : IRequest<ApiResult<CertificatesDto>>
  {
    public string Id { get; set; }
  }
}
