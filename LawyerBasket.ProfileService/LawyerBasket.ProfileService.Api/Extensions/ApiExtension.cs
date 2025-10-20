using LawyerBasket.ProfileService.Application.Contracts.Api;

namespace LawyerBasket.ProfileService.Api.Extensions
{
  public static class ApiExtension
  {
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
      services.AddScoped<ICurrentUserService, CurrentUserService>();
      return services;
    }
  }
}
