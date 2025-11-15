using LawyerBasket.Shared.Common.Domain;

namespace LawyerBasket.ProfileService.Domain.Entities
{
    public class LawyerProfile : Entity
    {
        public string UserProfileId { get; set; } = default!;
        public UserProfile? UserProfile { get; set; }
        // Mesleki bilgiler
        public string BarAssociation { get; set; } = default!;   // Baro adı (örn: İstanbul Barosu)
        public string BarNumber { get; set; } = default!;        // Baro kayıt numarası
        public string LicenseNumber { get; set; } = default!;    // Avukatlık ruhsat numarası
        public DateTime LicenseDate { get; set; }                // Ruhsat tarihi
        public string? About { get; set; }                       // Avukat hakkında yazısı

        // Uzmanlık alanları
        public List<LawyerExpertisement>? LawyerExpertisements { get; set; }
        public List<Experience>? Experience { get; set; }
        public List<Academy>? Academy { get; set; }
        public List<Certificates>? Certificates { get; set; }
        public Contact? Contact { get; set; }
    }
}
