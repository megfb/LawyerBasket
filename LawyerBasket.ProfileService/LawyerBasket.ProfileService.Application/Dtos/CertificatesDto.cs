namespace LawyerBasket.ProfileService.Application.Dtos
{
  public class CertificatesDto
  {
    public string Id { get; set; }
    public string LawyerProfileId { get; set; }
    public string Name { get; set; }
    public string Institution { get; set; }
    public DateTime DateReceived { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
  }
}
