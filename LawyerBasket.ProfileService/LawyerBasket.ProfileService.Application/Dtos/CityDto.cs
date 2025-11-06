namespace LawyerBasket.ProfileService.Application.Dtos
{
  public class CityDto
  {
    public string Name { get; set; } = default!;
    public List<AddressDto>? AddressDto { get; set; }
  }
}
