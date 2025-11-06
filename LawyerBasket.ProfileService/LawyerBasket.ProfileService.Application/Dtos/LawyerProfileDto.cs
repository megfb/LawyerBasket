using LawyerBasket.ProfileService.Domain.Entities;

namespace LawyerBasket.ProfileService.Application.Dtos
{
    public class LawyerProfileDto
    {
        public string Id { get; set; } = default!;
        public string UserProfileId { get; set; } = default!;
        public UserProfile? UserProfile { get; set; }
        public string BarAssociation { get; set; } = default!;   // Baro adı (örn: İstanbul Barosu)
        public string BarNumber { get; set; } = default!;        // Baro kayıt numarası
        public string LicenseNumber { get; set; } = default!;    // Avukatlık ruhsat numarası
        public DateTime LicenseDate { get; set; }                // Ruhsat tarihi
        public List<LawyerExpertisement>? LawyerExpertisements { get; set; }
        public List<Experience>? Experience { get; set; }
        public List<Academy>? Academy { get; set; }
        public List<Certificates>? Certificates { get; set; }
        public Contact? Contact { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
