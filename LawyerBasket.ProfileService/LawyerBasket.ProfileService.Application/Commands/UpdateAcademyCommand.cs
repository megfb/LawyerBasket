using LawyerBasket.ProfileService.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerBasket.ProfileService.Application.Commands
{
  public class UpdateAcademyCommand:IRequest<ApiResult<AcademyDto>>
  {
    public string Id { get; set; }
    public string University { get; set; }
    public string? Degree { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public DateTime UpdatedAt { get; set; }
  }
}
