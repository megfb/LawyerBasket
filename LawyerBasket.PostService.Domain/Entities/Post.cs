using LawyerBasket.PostService.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerBasket.PostService.Domain.Entities
{
  public class Post:Entity
  {
    public string Content { get; set; }
    public List<string> Likes { get; set; }
    public List<Comment> Comments { get; set; }

  }
}
