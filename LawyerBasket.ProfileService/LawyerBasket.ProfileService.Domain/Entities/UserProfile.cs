using LawyerBasket.Shared.Common.Domain;

namespace LawyerBasket.ProfileService.Domain.Entities
{
    public class UserProfile : Entity
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string? PhoneNumber { get; set; }
        public Gender Gender { get; set; }
        public string GenderId { get; set; } = default!;
        public DateTime? BirthDate { get; set; }
        public string? NationalId { get; set; }
        public Address? Address { get; set; }
        public UserType UserType { get; set; }
        public LawyerProfile? LawyerProfile { get; set; }
        //public ClientProfile? ClientProfile { get; set; }

    }

    public enum UserType
    {
        Undefined = 0,
        Lawyer = 1,
        Client = 2
    }
}
