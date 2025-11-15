
using LawyerBasket.Shared.Common.Domain;

namespace LawyerBasket.ProfileService.Domain.Entities
{
    public class Certificates : Entity
    {
        public LawyerProfile? LawyerProfile { get; set; }
        public string LawyerProfileId { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Institution { get; set; } = default!;
        public DateTime DateReceived { get; set; }
        public string? Description { get; set; }

    }
}
