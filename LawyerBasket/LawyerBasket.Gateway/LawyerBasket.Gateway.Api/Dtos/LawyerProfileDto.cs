namespace LawyerBasket.Gateway.Api.Dtos
{
    public class LawyerProfileDto
    {
        public string Id { get; set; } = default!;
        public string UserProfileId { get; set; } = default!;
        public string BarAssociation { get; set; } = default!;
        public string BarNumber { get; set; } = default!;
        public string LicenseNumber { get; set; } = default!;
        public DateTime LicenseDate { get; set; }
        public string? About { get; set; }
        public List<LawyerExpertisementDto>? LawyerExpertisements { get; set; }
        public List<ExperienceDto>? Experience { get; set; }
        public List<AcademyDto>? Academy { get; set; }
        public List<CertificatesDto>? Certificates { get; set; }
        public ContactDto? Contact { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

