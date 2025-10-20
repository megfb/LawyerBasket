using MediatR;

namespace LawyerBasket.ProfileService.Application.Commands
{
  public class CreateContactCommand : IRequest<ApiResult<string>>
  {
    public string LawyerProfileId { get; set; }
    public string PhoneNumber { get; set; }
    public string? AlternatePhoneNumber { get; set; }
    public string Email { get; set; }
    public string? AlternateEmail { get; set; }
    public string? Website { get; set; }
  }
}


