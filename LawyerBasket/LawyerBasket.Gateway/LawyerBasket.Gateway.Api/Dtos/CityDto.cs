namespace LawyerBasket.Gateway.Api.Dtos
{
    public class CityDto
    {
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}

