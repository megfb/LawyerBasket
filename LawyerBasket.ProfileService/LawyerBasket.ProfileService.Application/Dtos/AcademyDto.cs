namespace LawyerBasket.ProfileService.Application.Dtos
{
    public class AcademyDto
    {
        public string Id { get; set; } = default!;
        public string LawyerProfileId { get; set; } = default!;
        public string University { get; set; } = default!;
        public string? Degree { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
