using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LawyerBasket.ProfileService.Application.Contracts.Api
{
  public interface ICurrentUserService
  {
    string? UserId { get; }
  }
}
