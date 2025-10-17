using LawyerBasket.AuthService.Application.Contracts.Infrastructure;
using LawyerBasket.AuthService.Domain;
using LawyerBasket.AuthService.Infrastructure.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LawyerBasket.AuthService.Infrastructure.Extensions
{
    public static class InfrastructureExtension
    {
        public static IServiceCollection AddInfraDIContainer(this IServiceCollection services, IConfiguration configuration)
        {
            var tokenOptions = configuration.GetSection("TokenOption").Get<CustomTokenOption>();
            if (tokenOptions != null)
            {
                services.Configure<CustomTokenOption>(options =>
                {
                    options.Issuer = tokenOptions.Issuer;
                    options.Audience = tokenOptions.Audience;
                    options.ExpiryMinutes = tokenOptions.ExpiryMinutes;
                    options.SecurityKey = tokenOptions.SecurityKey;
                });
            }

            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<ITokenService, TokenService>();
            return services;
        }
    }
}
