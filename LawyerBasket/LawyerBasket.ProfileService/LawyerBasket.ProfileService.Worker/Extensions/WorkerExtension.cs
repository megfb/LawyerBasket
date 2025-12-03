using LawyerBasket.ProfileService.Worker.BackgroundServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerBasket.ProfileService.Worker.Extensions
{
  public static class WorkerExtension
  {
    public static IServiceCollection AddWorkerServices(this IServiceCollection services, IConfiguration configuration)
    {
      services.AddHostedService<ProfileMessageWorker>();
      return services;
    }
  }
}
