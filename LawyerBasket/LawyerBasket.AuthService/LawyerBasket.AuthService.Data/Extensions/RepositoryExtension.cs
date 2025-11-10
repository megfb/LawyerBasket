using LawyerBasket.AuthService.Application.Contracts.Data;
using LawyerBasket.AuthService.Data.AppRole;
using LawyerBasket.AuthService.Data.AppUser;
using LawyerBasket.AuthService.Data.AppUserRole;
using LawyerBasket.Shared.Common.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LawyerBasket.AuthService.Data.Extensions
{
    public static class RepositoryExtension
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                var connectionString = configuration.GetSection(ConnectionStringOption.Key).Get<ConnectionStringOption>();
                options.UseNpgsql(connectionString!.PostgreSql);
            });

            services.AddScoped<IAppRoleRepository, AppRoleRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAppUserRepository, AppUserRepository>();
            services.AddScoped<IAppUserRoleRepository, AppUserRoleRepository>();
            return services;
        }
    }
}
