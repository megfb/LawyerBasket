using LawyerBasket.AuthService.Domain.Entities.Common;

namespace LawyerBasket.AuthService.Domain.Entities
{
    public class AppRole : Entity
    {
        public string Name { get; set; } // e.g., "Lawyer", "Client"
        public string Description { get; set; }
        public List<AppUserRole> AppUserRole { get; set; }
    }
}
