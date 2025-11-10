namespace LawyerBasket.Shared.Common.Domain
{
    public abstract class Entity : IEntity
    {
        public string Id { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
