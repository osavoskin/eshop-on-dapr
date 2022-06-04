using Dapr.Client;
using EShop.WebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EShop.WebApp.Services
{
    public class StateStore : IStateStore
    {
        const string Store = "statestore";
        const string ItemsKey = "items-key";

        private readonly DaprClient daprClient;

        public StateStore(DaprClient daprClient)
        {
            this.daprClient = daprClient;
        }

        public Task<IEnumerable<CatalogItem>> TryGetItems()
        {
            return daprClient.GetStateAsync<IEnumerable<CatalogItem>>(Store, ItemsKey);
        }

        public Task SaveItems(IEnumerable<CatalogItem> items)
        {
            return daprClient.SaveStateAsync(Store, ItemsKey, items, null, 
                new Dictionary<string, string>
                {
                    ["ttlInSeconds"] = "120"
                });
        }

        public Task ClearStore()
        {
            return daprClient.DeleteStateAsync(Store, ItemsKey);
        }
    }
}
