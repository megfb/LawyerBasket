using LawyerBasket.Shared.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerBasket.ProfileService.Domain.Entities
{
  public class OutboxMessage:IEntity
  {
    public string Id { get; set; }

    //entity id olacak
    public string AggregateId { get; set; } = default!;
    // routing key olacak
    public string Type { get; set; } = default!;
    //json formatında olacak
    public string Payload { get; set; } = default!;
    //mesaj durumu
    public Status Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ProcessedAt { get; set; }
    public string? Error { get; set; }

  }

  public enum Status
  {
    Pending = 0,
    Processed = 1,
    Failed = 2
  }
}
