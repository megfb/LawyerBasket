using LawyerBasket.ProfileService.Application.Dtos;
using LawyerBasket.Shared.Common.Response;
using MediatR;

namespace LawyerBasket.ProfileService.Application.Commands
{
    public class UpdateUserProfileCommand : IRequest<ApiResult<UserProfileDto>>
    {
        public string Id { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string? PhoneNumber { get; set; }
        public string GenderId { get; set; } = default!;
        public DateTime? BirthDate { get; set; }
        public string? NationalId { get; set; }
    }
}
