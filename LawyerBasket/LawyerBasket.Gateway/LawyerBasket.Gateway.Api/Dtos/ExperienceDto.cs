namespace LawyerBasket.Gateway.Api.Dtos
{
    public class ExperienceDto
    {
        public string Id { get; set; } = default!;
        public string LawyerProfileId { get; set; } = default!;
        public string CompanyName { get; set; } = default!;
        public string Position { get; set; } = default!;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Description { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}

