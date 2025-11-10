using LawyerBasket.PostService.Application.Contracts.Api;

namespace LawyerBasket.PostService.Api.Extensions
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
