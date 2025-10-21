namespace LawyerBasket.ProfileService.Application.Dtos
{
  public class AddressDto
  {
    public string Id { get; set; }
    public string UserProfileId { get; set; }
    public UserProfileDto UserProfileDto { get; set; }
    public string AddressLine { get; set; }
    public CityDto CityDto { get; set; }
    public string CityId { get; set; }
  }
}
