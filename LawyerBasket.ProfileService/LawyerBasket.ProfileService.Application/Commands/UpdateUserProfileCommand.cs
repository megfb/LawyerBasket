using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LawyerBasket.ProfileService.Application.Dtos;
using LawyerBasket.ProfileService.Domain.Entities;
using MediatR;

namespace LawyerBasket.ProfileService.Application.Commands
{
  public class UpdateUserProfileCommand:IRequest<ApiResult<UserProfileDto>>
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
