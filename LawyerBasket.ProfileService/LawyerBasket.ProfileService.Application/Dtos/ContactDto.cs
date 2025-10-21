namespace LawyerBasket.ProfileService.Application.Dtos
{
  public class ContactDto
  {
    public string Id { get; set; }
    public string LawyerProfileId { get; set; }
    public string PhoneNumber { get; set; } = default!;
    public string? AlternatePhoneNumber { get; set; }
    public string Email { get; set; } = default!;
    public string? AlternateEmail { get; set; }
    public string? Website { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
  }
}
