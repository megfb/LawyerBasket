using LawyerBasket.ProfileService.Application.Dtos;
using LawyerBasket.Shared.Common.Response;
using MediatR;

namespace LawyerBasket.ProfileService.Application.Commands
{
  public class UpdateContactCommand : IRequest<ApiResult<ContactDto>>
  {
    public string Id { get; set; }
    public string PhoneNumber { get; set; }
    public string? AlternatePhoneNumber { get; set; }
    public string Email { get; set; }
    public string? AlternateEmail { get; set; }
    public string? Website { get; set; }
    public DateTime UpdatedAt { get; set; }
  }
}
