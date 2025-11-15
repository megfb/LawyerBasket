namespace LawyerBasket.Gateway.Api.Dtos
{
    public class UserProfileWDetailsDto
    {
        public string Id { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string? PhoneNumber { get; set; }
        public GenderDto? Gender { get; set; }
        public string GenderId { get; set; } = default!;
        public DateTime? BirthDate { get; set; }
        public string? NationalId { get; set; }
        public AddressDto? Address { get; set; }
        public int UserType { get; set; }
        public LawyerProfileDto? LawyerProfile { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}

