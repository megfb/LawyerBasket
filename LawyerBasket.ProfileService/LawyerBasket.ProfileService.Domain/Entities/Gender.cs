using LawyerBasket.Shared.Common.Domain;

namespace LawyerBasket.ProfileService.Domain.Entities
{
    public class Gender : Entity
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public List<UserProfile>? UserProfile { get; set; }
    }
}
