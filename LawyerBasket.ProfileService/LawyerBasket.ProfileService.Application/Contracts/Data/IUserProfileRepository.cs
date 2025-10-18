using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerBasket.ProfileService.Application.Contracts.Data
{
  public interface IUserProfileRepository:IGenericRepository<Domain.Entities.UserProfile>
  {
  }
}
