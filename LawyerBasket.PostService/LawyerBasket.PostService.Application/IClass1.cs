using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LawyerBasket.PostService.Application
{
  public interface IClass1
  {
    Task<HttpStatusCode> AddToDb(Domain.Entities.Post post);
  }
}
