using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerBasket.ProfileService.Data
{
  public class ConnectionStringOption
  {
    public const string Key = "ConnectionStrings";
    public string PostgreSql { get; set; }
  }
}
