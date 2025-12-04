using LawyerBasket.ProfileService.Domain.Entities;
using LawyerBasket.Shared.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerBasket.ProfileService.Application.Contracts.Data
{
  public interface IOutboxMessageRepository : IGenericRepository<OutboxMessage>
  {

    Task<List<OutboxMessage>> GetPendingMessagesAsync(CancellationToken cancellationToken);
  }
}
