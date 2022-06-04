using EShop.WebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EShop.WebApp.Services
{
    public interface IStateStore
    {
        Task<IEnumerable<CatalogItem>> TryGetItems();

        Task SaveItems(IEnumerable<CatalogItem> items);

        Task ClearStore();
    }
}
