using LawyerBasket.ProfileService.Application.Contracts.Data;
using LawyerBasket.ProfileService.Data.Academy;
using LawyerBasket.ProfileService.Data.Address;
using LawyerBasket.ProfileService.Data.Certificates;
using LawyerBasket.ProfileService.Data.City;
using LawyerBasket.ProfileService.Data.Contact;
using LawyerBasket.ProfileService.Data.Experience;
using LawyerBasket.ProfileService.Data.Expertisement;
using LawyerBasket.ProfileService.Data.Gender;
using LawyerBasket.ProfileService.Data.LawyerExpertisement;
using LawyerBasket.ProfileService.Data.LawyerProfile;
using LawyerBasket.ProfileService.Data.UserProfile;
using LawyerBasket.Shared.Common.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LawyerBasket.ProfileService.Data.Extensions
{
  public static class RepositoryExtension
  {
    public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
      services.AddDbContext<AppDbContext>(options =>
      {
        var connectionString = configuration.GetSection(ConnectionStringOption.Key).Get<ConnectionStringOption>();
        options.UseNpgsql(connectionString.PostgreSql);
      });


      services.AddScoped<IAcademyRepository, AcademyRepository>();
      services.AddScoped<IAddressRepository, AddressRepository>();
      services.AddScoped<ICertificateRepository, CertificateRepository>();
      services.AddScoped<ICityRepository, CityRepository>();
      services.AddScoped<IContactRepository, ContactRepository>();
      services.AddScoped<IExperienceRepository, ExperienceRepository>();
      services.AddScoped<IExpertisemenetRepository, ExpertisementRepository>();
      services.AddScoped<IGenderRepository, GenderRepository>();
      services.AddScoped<ILawyerExpertisementRepository, LawyerExpertisementRepository>();
      services.AddScoped<ILawyerProfileRepository, LawyerProfileRepository>();
      services.AddScoped<IUserProfileRepository, UserProfileRepository>();
      services.AddScoped<IUnitOfWork, UnitOfWork>();

      return services;
    }
  }
}
