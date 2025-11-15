namespace LawyerBasket.ProfileService.Application.Dtos
{
  public class CertificatesDto
  {
    public string Id { get; set; } = default!;
    public string LawyerProfileId { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Institution { get; set; } = default!;
    public DateTime DateReceived { get; set; }
    public string? Description { get; set; }                  // Sertifika eğitiminin içeriği
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
  }
}
