using LawyerBasket.ProfileService.Application.Dtos;
using LawyerBasket.ProfileService.Domain.Entities;
using LawyerBasket.Shared.Common.Response;
using MediatR;

namespace LawyerBasket.ProfileService.Application.Commands
{
  public class CreateUserProfileCommand : IRequest<ApiResult<UserProfileDto>>
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string GenderId { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? NationalId { get; set; }
    public UserType UserType { get; set; }
  }
}

