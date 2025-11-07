namespace LawyerBasket.GatewayTest.Dtos.ProfileService
{
    public class LawyerProfileDto
    {
        public string Id { get; set; } = default!;
        public string UserProfileId { get; set; } = default!;
        public UserProfileDto? UserProfileDto { get; set; }
        public string BarAssociation { get; set; } = default!;   // Baro adı (örn: İstanbul Barosu)
        public string BarNumber { get; set; } = default!;        // Baro kayıt numarası
        public string LicenseNumber { get; set; } = default!;    // Avukatlık ruhsat numarası
        public DateTime LicenseDate { get; set; }                // Ruhsat tarihi
        public List<LawyerExpertisementDto>? LawyerExpertisementsDto { get; set; }
        public List<ExperienceDto>? ExperienceDto { get; set; }
        public List<AcademyDto>? AcademyDto { get; set; }
        public List<CertificatesDto>? CertificatesDto { get; set; }
        public ContactDto? ContactDto { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
