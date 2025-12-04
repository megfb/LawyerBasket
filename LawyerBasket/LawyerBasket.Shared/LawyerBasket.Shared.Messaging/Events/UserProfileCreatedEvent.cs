using LawyerBasket.Shared.Messaging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerBasket.Shared.Messaging.Events
{
  public class UserProfileCreatedEvent : IIntegrationEvent
  {
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public UserType UserType { get; set; }
  }

  public enum UserType
  {
    Undefined = 0,
    Lawyer = 1,
    Client = 2
  }
}
