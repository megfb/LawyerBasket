using LawyerBasket.AuthService.Domain.Entities.Common;

namespace LawyerBasket.AuthService.Domain.Entities
{
    public class AppUserRole : Entity
    {
        public AppUser AppUser { get; set; }
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public AppRole AppRole { get; set; }
    }
}
