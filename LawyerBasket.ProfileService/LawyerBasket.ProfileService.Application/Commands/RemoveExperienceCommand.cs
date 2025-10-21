using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerBasket.ProfileService.Application.Commands
{
  public class RemoveExperienceCommand:IRequest<ApiResult>
  {
    public string Id { get; set; }
  }
}
