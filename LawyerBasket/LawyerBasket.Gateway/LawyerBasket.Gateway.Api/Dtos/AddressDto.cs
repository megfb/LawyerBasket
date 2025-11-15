namespace LawyerBasket.Gateway.Api.Dtos
{
    public class AddressDto
    {
        public string Id { get; set; } = default!;
        public string UserProfileId { get; set; } = default!;
        public string AddressLine { get; set; } = default!;
        public CityDto? City { get; set; }
        public string CityId { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}

