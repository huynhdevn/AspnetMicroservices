using Shopping.Aggregator.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopping.Aggregator.Services
{
    public interface ICatalogService
    {
        Task<IEnumerable<CatalogModel>> GeCatalog();
        Task<IEnumerable<CatalogModel>> GeCatalogByCategory(string category);
        Task<CatalogModel> GeCatalog(string id);
    }
}
