using LawyerBasket.AuthService.Application.Contracts.Api;

namespace LawyerBasket.AuthService.Api.Extensions
{
  public static class ApiExtension
  {
    public static IServiceCollection AddApiExtension(this IServiceCollection services, IConfiguration configuration)
    {
      services.AddScoped<ICurrentUserService, CurrentUserService>();
      return services;
    }
  }
}
