using LawyerBasket.ProfileService.Application.Dtos;
using LawyerBasket.Shared.Common.Response;
using MediatR;

namespace LawyerBasket.ProfileService.Application.Commands
{
    public class CreateCertificateCommand : IRequest<ApiResult<CertificatesDto>>
    {
        public string LawyerProfileId { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Institution { get; set; } = default!;
        public DateTime DateReceived { get; set; }
        public string? Description { get; set; }
    }
}


