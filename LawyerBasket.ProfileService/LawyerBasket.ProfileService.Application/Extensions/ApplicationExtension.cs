using FluentValidation;
using LawyerBasket.ProfileService.Application.Validators;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace LawyerBasket.ProfileService.Application.Extensions
{
  public static class ApplicationExtension
  {
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
      services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
      services.AddAutoMapper(cfg => { }, Assembly.GetExecutingAssembly());
      services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
      services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

      return services;
    }
  }
}
