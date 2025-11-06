using LawyerBasket.Shared.Common.Domain;

namespace LawyerBasket.AuthService.Domain.Entities
{
    public class AppUser : Entity
    {
        public string Email { get; set; } = default!;
        public string PasswordHash { get; set; } = default!;
        public DateTime? LastLoginAt { get; set; }
        public List<AppUserRole>? AppUserRole { get; set; }
    }
}
