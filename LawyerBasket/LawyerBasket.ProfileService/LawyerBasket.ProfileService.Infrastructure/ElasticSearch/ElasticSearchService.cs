using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Nodes;
using LawyerBasket.ProfileService.Application.Contracts.Infrastructure;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerBasket.ProfileService.Infrastructure.ElasticSearch
{
  public class ElasticSearchService<T> : IElasticSearchService<T> where T : class
  {
    private readonly ElasticsearchClient _client;
    private readonly ILogger<ElasticSearchService<T>> _logger;

    public ElasticSearchService(ElasticsearchClient client, ILogger<ElasticSearchService<T>> logger)
    {
      _client = client;
      _logger = logger;
    }
    public async Task<bool> CreateOrUpdateAsync(T document, string indexName)
    {
      try
      {
        // 1. Generic olan 'document' nesnesinin içindeki "Id" property'sinin değerini okuyoruz.
        // (Reflection kullanarak)
        var idValue = document.GetType().GetProperty("Id")?.GetValue(document)?.ToString();

        var response = await _client.IndexAsync(document, idx => idx
            .Index(indexName)
            .Id(idValue));

        if (!response.IsValidResponse)
        {
          // Buraya loglama eklenebilir: response.DebugInformation
          return false;
        }
        return true;
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Elasticsearch'te belge oluşturulurken/güncellenirken hata oluştu.");
        throw ex;
      }

    }

    public Task<T?> GetAsync(string id, string indexName)
    {
      throw new NotImplementedException();
    }

    public Task<List<T>> SearchAsync(string keyword, string indexName)
    {
      throw new NotImplementedException();
    }
  }
}
