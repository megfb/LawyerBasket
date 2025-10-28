using LawyerBasket.PostService.Application.Contracts.Data;
using LawyerBasket.PostService.Data.Post;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;


namespace LawyerBasket.PostService.Data.Extensions
{
  public static class RepositoryExtension
  {
    public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {

      services.AddScoped<IPostRepository, PostRepository>();
      services.AddScoped<IUnitOfWork, UnitOfWork>();

      services.AddOptions<MongoOption>().BindConfiguration(nameof(MongoOption)).ValidateDataAnnotations()
          .ValidateOnStart();


      services.AddSingleton(sp => sp.GetRequiredService<IOptions<MongoOption>>().Value);
      services.AddSingleton<IMongoClient, MongoClient>(sp =>
      {
        var options = sp.GetRequiredService<MongoOption>();
        return new MongoClient(options.ConnectionString);
      });

      services.AddScoped(sp =>
      {
        var mongoClient = sp.GetRequiredService<IMongoClient>();
        var options = sp.GetRequiredService<MongoOption>();

        return AppDbContext.Create(mongoClient.GetDatabase(options.DatabaseName));
      });

      return services;
    }
  }
}
