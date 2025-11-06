using LawyerBasket.ProfileService.Application.Dtos;
using LawyerBasket.Shared.Common.Response;
using MediatR;

namespace LawyerBasket.ProfileService.Application.Commands
{
    public class UpdateLawyerProfileCommand : IRequest<ApiResult<LawyerProfileDto>>
    {
        public string Id { get; set; } = default!;
        public string UserProfileId { get; set; } = default!;
        public string BarAssociation { get; set; } = default!;
        public string BarNumber { get; set; } = default!;
        public string LicenseNumber { get; set; } = default!;
        public DateTime LicenseDate { get; set; }
    }
}
