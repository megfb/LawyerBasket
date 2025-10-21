using LawyerBasket.ProfileService.Application.Dtos;
using MediatR;

namespace LawyerBasket.ProfileService.Application.Commands
{
  public class UpdateUserProfileCommand : IRequest<ApiResult<UserProfileDto>>
  {
    public string Id { get; set; }
    public string UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string GenderId { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? NationalId { get; set; }
  }
}
