namespace LawyerBasket.Gateway.Api.Dtos
{
    public class ContactDto
    {
        public string Id { get; set; } = default!;
        public string LawyerProfileId { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string? AlternatePhoneNumber { get; set; }
        public string Email { get; set; } = default!;
        public string? AlternateEmail { get; set; }
        public string? Website { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

