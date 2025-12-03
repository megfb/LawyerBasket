using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerBasket.ProfileService.Worker.BackgroundServices
{
  public class ProfileMessageWorker : BackgroundService
  {
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
      throw new NotImplementedException();
    }
  }
}
