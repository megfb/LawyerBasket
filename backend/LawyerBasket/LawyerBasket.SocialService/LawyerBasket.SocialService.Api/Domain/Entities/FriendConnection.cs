using LawyerBasket.Shared.Common.Domain;

namespace LawyerBasket.SocialService.Api.Domain.Entities
{
    public class FriendConnection : Entity
    {
        public string SenderId { get; set; } = default!;
        public string ReceiverId { get; set; } = default!;
        public Status Status { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime? AcceptedDate { get; set; }
        public DateTime? RejectedDate { get; set; }
    }
    public enum Status
    {
        Pending = 0,
        Accepted = 1,
        Rejected = 2,
        Cancelled = 3
    }
}
