using Dapr.Client;
using EShop.Common.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace EShop.Common.Clients
{
    public class CatalogClient : ICatalogClient
    {
        private readonly DaprClient daprClient;

        public CatalogClient(DaprClient daprClient)
        {
            this.daprClient = daprClient;
        }

        public Task<IEnumerable<Item>> GetItems()
        {
            return daprClient.InvokeMethodAsync<IEnumerable<Item>>(HttpMethod.Get, "catalog", "Items");
        }
    }
}
