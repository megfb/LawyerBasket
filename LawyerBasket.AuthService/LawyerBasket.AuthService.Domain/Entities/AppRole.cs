
using LawyerBasket.Shared.Common.Domain;

namespace LawyerBasket.AuthService.Domain.Entities
{
    public class AppRole : Entity
    {
        public string Name { get; set; } = default!; // e.g., "Lawyer", "Client"
        public string Description { get; set; } = default!;
        public List<AppUserRole>? AppUserRole { get; set; }
    }
}
