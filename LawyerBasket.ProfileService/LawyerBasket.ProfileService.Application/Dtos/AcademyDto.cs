using LawyerBasket.ProfileService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerBasket.ProfileService.Application.Dtos
{
  public class AcademyDto
  {
    public string Id { get; set; }
    public string LawyerProfileId { get; set; }
    public string University { get; set; }
    public string? Degree { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
  }
}
