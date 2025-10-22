using FluentValidation;
using LawyerBasket.ProfileService.Data;
using LawyerBasket.SocialService.Api.Application.Validators;
using LawyerBasket.SocialService.Api.Domain.Contracts.Data;
using LawyerBasket.SocialService.Api.Domain.Repositories;
using LawyerBasket.SocialService.Api.Domain.Repositories.EntityFramework;
using LawyerBasket.SocialService.Api.Domain.Repositories.EntityFramework.DbContexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace LawyerBasket.SocialService.Api.Extensions
{
  public static class ServiceExtension
  {
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
      services.AddDbContext<AppDbContext>(options =>
      {
        var connectionString = configuration.GetSection(ConnectionStringOption.Key).Get<ConnectionStringOption>();
        options.UseNpgsql(connectionString.PostgreSql);
      });


      services.AddScoped<IUnitOfWork, UnitOfWork>();
      services.AddScoped<IFriendConnectionRepository, FriendConnectionRepository>();
      services.AddScoped<IFriendshipRepository, FriendshipRepository>();
      services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
      services.AddAutoMapper(cfg => { }, Assembly.GetExecutingAssembly());
      services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
      services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

      return services;
    }
  }
}
