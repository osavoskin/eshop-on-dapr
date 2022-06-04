using EShop.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EShop.Common.Clients
{
    public interface ICatalogClient
    {
        Task<IEnumerable<Item>> GetItems();
    }
}
