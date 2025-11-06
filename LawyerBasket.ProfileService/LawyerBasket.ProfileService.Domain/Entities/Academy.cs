using LawyerBasket.Shared.Common.Domain;

namespace LawyerBasket.ProfileService.Domain.Entities
{
    public class Academy : Entity
    {
        public LawyerProfile? LawyerProfile { get; set; }
        public string LawyerProfileId { get; set; } = default!;
        public string University { get; set; } = default!;
        public string? Degree { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

    }
}
