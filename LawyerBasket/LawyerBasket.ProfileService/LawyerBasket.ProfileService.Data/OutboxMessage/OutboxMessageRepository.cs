using LawyerBasket.ProfileService.Application.Contracts.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerBasket.ProfileService.Data.OutboxMessage
{
  internal class OutboxMessageRepository(AppDbContext appDbContext) : GenericRepository<Domain.Entities.OutboxMessage>(appDbContext), IOutboxMessageRepository
  {
  }
}
