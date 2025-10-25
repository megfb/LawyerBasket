using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerBasket.PostService.Data
{
  public class MongoOption
  {
    public string DatabaseName { get; set; }
     public string ConnectionString { get; set; }
  }
}
