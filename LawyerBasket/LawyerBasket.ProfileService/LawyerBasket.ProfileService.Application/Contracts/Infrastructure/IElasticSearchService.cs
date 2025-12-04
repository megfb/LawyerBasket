using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerBasket.ProfileService.Application.Contracts.Infrastructure
{
  public interface IElasticSearchService<T>
  {
    // Yeni bir kayıt ekler veya var olanı günceller
    Task<bool> CreateOrUpdateAsync(T document, string indexName);

    // Tek bir dökümanı ID ile getirir
    Task<T?> GetAsync(string id, string indexName);

    // Basit bir arama yapar (Örnek)
    Task<List<T>> SearchAsync(string keyword, string indexName);
  }
}
