using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LawyerBasket.ProfileService.Domain.Entities.Common;

namespace LawyerBasket.ProfileService.Domain.Entities
{
  public class LawyerExpertisement:Entity
  {
    public LawyerProfile LawyerProfile { get; set; }
    public string LawyerProfileId { get; set; }
    public Expertisement Expertisement { get; set; }
    public string ExpertisementId { get; set; }
  }
}
