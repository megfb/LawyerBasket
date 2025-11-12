using LawyerBasket.ProfileService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerBasket.ProfileService.Application.Dtos
{
  public class UserProfileWDetailsDto
  {
    public string Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string? PhoneNumber { get; set; }
    public GenderDto Gender { get; set; }
    public string GenderId { get; set; } = default!;
    public DateTime? BirthDate { get; set; }
    public string? NationalId { get; set; }
    public AddressDto? Address { get; set; }
    public UserType UserType { get; set; }
    public LawyerProfileDto? LawyerProfile { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    //public ClientProfile? ClientProfile { get; set; }
  }
}
