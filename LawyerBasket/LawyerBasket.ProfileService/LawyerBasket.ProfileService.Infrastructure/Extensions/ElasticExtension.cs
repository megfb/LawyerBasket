using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using LawyerBasket.ProfileService.Application.Contracts.Infrastructure;
using LawyerBasket.ProfileService.Infrastructure.ElasticSearch;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerBasket.ProfileService.Infrastructure.Extensions
{
  public static class ElasticExtension
  {
    public static IServiceCollection AddElasticSearch(this IServiceCollection services, IConfiguration configuration)
    {
      // 1. Ayarları al
      var elasticUri = configuration["ElasticSearch:Url"];
      var defaultIndex = configuration["ElasticSearch:DefaultIndex"];
      var username = configuration["ElasticSearch:Username"];
      var password = configuration["ElasticSearch:Password"];

      // 2. Client Ayarlarını Yapılandır
      var settings = new ElasticsearchClientSettings(new Uri(elasticUri))
          .DefaultIndex(defaultIndex).Authentication(new BasicAuthentication(username, password)) // <--- EKLENECEK KISIM
    .ServerCertificateValidationCallback(CertificateValidations.AllowAll) // SSL hatası almamak için (Localhost için)
    .PrettyJson();

      // 3. Client'ı Singleton olarak kaydet (Önerilen yöntem)
      var client = new ElasticsearchClient(settings);
      services.AddSingleton(client);

      // 4. Kendi servisimizi Scoped olarak kaydet
      services.AddScoped(typeof(IElasticSearchService<>), typeof(ElasticSearchService<>));
      return services;
    }
  }
}
