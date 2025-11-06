using LawyerBasket.ProfileService.Application.Dtos;
using LawyerBasket.ProfileService.Domain.Entities;
using LawyerBasket.Shared.Common.Response;
using MediatR;

namespace LawyerBasket.ProfileService.Application.Commands
{
  public class CreateUserProfileCommand : IRequest<ApiResult<UserProfileDto>>
  {
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string? PhoneNumber { get; set; }
    public string GenderId { get; set; } = default!;
    public DateTime? BirthDate { get; set; }
    public string? NationalId { get; set; }
    public UserType UserType { get; set; }
  }
}

