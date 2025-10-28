using LawyerBasket.PostService.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerBasket.PostService.Domain.Entities
{
  public class Likes:Entity
  {
    public string UserId { get; set; }
    public string PostId { get; set; }
  }
}
