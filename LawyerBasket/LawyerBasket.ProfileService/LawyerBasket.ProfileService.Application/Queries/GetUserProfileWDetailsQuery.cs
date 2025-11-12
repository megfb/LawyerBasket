using LawyerBasket.ProfileService.Application.Dtos;
using LawyerBasket.Shared.Common.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerBasket.ProfileService.Application.Queries
{
  public class GetUserProfileWDetailsQuery : IRequest<ApiResult<UserProfileWDetailsDto>>
  {
  }
}
