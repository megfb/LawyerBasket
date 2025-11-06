using LawyerBasket.Shared.Common.Domain;

namespace LawyerBasket.ProfileService.Domain.Entities
{
    public class Experience : Entity
    {
        public LawyerProfile? LawyerProfile { get; set; }
        public string LawyerProfileId { get; set; } = default!;
        public string CompanyName { get; set; } = default!;
        public string Position { get; set; } = default!;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Description { get; set; } = default!;

    }
}
