using LawyerBasket.SocialService.Api.Domain.Entities.Common;

namespace LawyerBasket.SocialService.Api.Domain.Entities
{
  public class FriendConnection : Entity
  {
    public string SenderId { get; set; }
    public string ReceiverId { get; set; }
    public Status Status { get; set; }
    public DateTime RequestDate { get; set; }
    public DateTime? AcceptedDate { get; set; }
    public DateTime? RejectedDate { get; set; }
    public DateTime CreatedAt { get; set; }
  }
  public enum Status
  {
    Pending = 0,
    Accepted = 1,
    Rejected = 2,
    Cancelled = 3
  }
}
