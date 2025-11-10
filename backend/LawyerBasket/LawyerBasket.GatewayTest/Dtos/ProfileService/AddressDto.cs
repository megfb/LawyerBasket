namespace LawyerBasket.GatewayTest.Dtos.ProfileService
{
    public class AddressDto
    {
        public string Id { get; set; } = default!;
        public string UserProfileId { get; set; } = default!;
        public UserProfileDto? UserProfileDto { get; set; }
        public string AddressLine { get; set; } = default!;
        public CityDto CityDto { get; set; } = default!;
        public string CityId { get; set; } = default!;
    }
}
