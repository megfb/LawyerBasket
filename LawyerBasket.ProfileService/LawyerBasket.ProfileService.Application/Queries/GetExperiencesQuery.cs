using LawyerBasket.ProfileService.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerBasket.ProfileService.Application.Queries
{
  public class GetExperiencesQuery: IRequest<ApiResult<List<ExperienceDto>>>
  {
  }
}
