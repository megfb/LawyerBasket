using FluentValidation;
using LawyerBasket.ProfileService.Data;
using LawyerBasket.SocialService.Api.Application.Validators;
using LawyerBasket.SocialService.Api.Domain.Contracts.Data;
using LawyerBasket.SocialService.Api.Domain.Repositories;
using LawyerBasket.SocialService.Api.Domain.Repositories.EntityFramework;
using LawyerBasket.SocialService.Api.Domain.Repositories.EntityFramework.DbContexts;
using LawyerBasket.SocialService.Api.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

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

      var tokenOptions = configuration.GetSection("TokenOption").Get<CustomTokenOption>();

      if (tokenOptions != null)
      {
        services.Configure<CustomTokenOption>(options =>
        {
          options.Issuer = tokenOptions.Issuer;
          options.Audience = tokenOptions.Audience; // No change here
          options.ExpiryMinutes = tokenOptions.ExpiryMinutes;
          options.SecurityKey = tokenOptions.SecurityKey;
        });

        services.AddAuthentication(options =>
        {
          options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
          options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
          options.TokenValidationParameters = new TokenValidationParameters
          {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = tokenOptions.Issuer,
            ValidAudience = tokenOptions.Audience.FirstOrDefault(), // Daha sonra kontrol edilecek
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey))
          };
        });

        services.AddAuthorization();
      }

      services.AddEndpointsApiExplorer();
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
          Title = "LawyerBasket AuthService API",
          Version = "v1"
        });

        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
          Name = "Authorization",
          Type = SecuritySchemeType.Http,
          Scheme = "bearer",
          BearerFormat = "JWT",
          In = ParameterLocation.Header,
          Description = "JWT token giriniz. Örnek: Bearer {token}"
        });

        c.AddSecurityRequirement(new OpenApiSecurityRequirement
          {
                            {
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Type = ReferenceType.SecurityScheme,
                                        Id = "Bearer"
                                    }
                                },
                                new string[] {}
                            }
          });
      });



      return services;
    }
  }
}
