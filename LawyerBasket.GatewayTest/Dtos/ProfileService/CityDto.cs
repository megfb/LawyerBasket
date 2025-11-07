namespace LawyerBasket.GatewayTest.Dtos.ProfileService
{
    public class CityDto
    {
        public string Name { get; set; } = default!;
        public List<AddressDto>? AddressDto { get; set; }
    }
}
