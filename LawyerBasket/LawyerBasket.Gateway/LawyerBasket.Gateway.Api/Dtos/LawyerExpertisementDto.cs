namespace LawyerBasket.Gateway.Api.Dtos
{
    public class LawyerExpertisementDto
    {
        public string Id { get; set; } = default!;
        public string LawyerProfileId { get; set; } = default!;
        public string ExpertisementId { get; set; } = default!;
        public ExpertisementDto? Expertisement { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}

