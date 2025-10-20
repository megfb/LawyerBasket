using MediatR;

namespace LawyerBasket.ProfileService.Application.Commands
{
  public class CreateCertificateCommand : IRequest<ApiResult<string>>
  {
    public string LawyerProfileId { get; set; }
    public string Name { get; set; }
    public string Institution { get; set; }
    public DateTime DateReceived { get; set; }
  }
}


