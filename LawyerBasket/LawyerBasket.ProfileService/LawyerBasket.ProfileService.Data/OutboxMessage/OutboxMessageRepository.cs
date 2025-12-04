using LawyerBasket.ProfileService.Application.Contracts.Data;
using LawyerBasket.ProfileService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerBasket.ProfileService.Data.OutboxMessage
{
  internal class OutboxMessageRepository(AppDbContext appDbContext) : GenericRepository<Domain.Entities.OutboxMessage>(appDbContext), IOutboxMessageRepository
  {
    public async Task<List<Domain.Entities.OutboxMessage>> GetPendingMessagesAsync(CancellationToken cancellationToken)
    {

      return await appDbContext.OutboxMessage.Where(x => x.Status == Status.Pending).OrderBy(x => x.CreatedAt).Take(20).ToListAsync(cancellationToken);
    }
  }
}
