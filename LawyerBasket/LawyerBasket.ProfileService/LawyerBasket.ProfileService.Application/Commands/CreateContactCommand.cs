using LawyerBasket.ProfileService.Application.Dtos;
using LawyerBasket.Shared.Common.Response;
using MediatR;

namespace LawyerBasket.ProfileService.Application.Commands
{
    public class CreateContactCommand : IRequest<ApiResult<ContactDto>>
    {
        public string LawyerProfileId { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string? AlternatePhoneNumber { get; set; }
        public string Email { get; set; } = default!;
        public string? AlternateEmail { get; set; }
        public string? Website { get; set; }
    }
}


