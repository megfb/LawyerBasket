namespace LawyerBasket.GatewayTest.Dtos.ProfileService
{
    public class CityDto
    {
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<AddressDto>? AddressDto { get; set; }
    }
}
