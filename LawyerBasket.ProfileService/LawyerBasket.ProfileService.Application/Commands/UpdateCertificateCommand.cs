using LawyerBasket.ProfileService.Application.Dtos;
using LawyerBasket.Shared.Common.Response;
using MediatR;

namespace LawyerBasket.ProfileService.Application.Commands
{
  public class UpdateCertificateCommand : IRequest<ApiResult<CertificatesDto>>
  {
    public string Id { get; set; }
    public string Name { get; set; }
    public string Institution { get; set; }
    public DateTime DateReceived { get; set; }
  }
}
