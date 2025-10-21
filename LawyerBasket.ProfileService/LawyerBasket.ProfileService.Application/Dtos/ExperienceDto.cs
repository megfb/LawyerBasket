namespace LawyerBasket.ProfileService.Application.Dtos
{
  public class ExperienceDto
  {
    public string Id { get; set; }
    public string LawyerProfileId { get; set; }
    public string CompanyName { get; set; }
    public string Position { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
  }
}
