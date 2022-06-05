using EShop.Common.Clients;
using EShop.Common.Models;
using EShop.WebApp.Models;
using EShop.WebApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> logger;
        private readonly ICatalogClient catalogClient;
        private readonly IOrderingClient orderingClient;
        private readonly IStateStore stateStore;

        public IEnumerable<CatalogItem> Items { get; private set; }

        public IndexModel(
            ILogger<IndexModel> logger,
            ICatalogClient catalogClient,
            IOrderingClient orderingClient,
            IStateStore stateStore)
        {
            this.logger = logger;
            this.catalogClient = catalogClient;
            this.orderingClient = orderingClient;
            this.stateStore = stateStore;
        }

        public async Task OnGet()
        {
            var cachedItems = await stateStore.TryGetItems();
            if (cachedItems is not null)
            {
                Items = cachedItems;
                return;
            }

            var random = new Random();
            var items = await catalogClient.GetItems();

            Items = items.Select(o => new CatalogItem
            {
                Id = o.Id,
                Price = o.Price,
                Title = o.Title,
                Count = random.Next(1, 10)
            });

            await stateStore.SaveItems(Items);
        }

        public async Task<IActionResult> OnPostSubmit(IEnumerable<CatalogItem> items)
        {
            await stateStore.ClearStore();

            await orderingClient.NotifyOrderSubmitted(new Order()
            {
                Items = items.Where(o => o.IsChecked)
                             .ToDictionary(o => o.Id.ToString(), o => o.Count)
            });

            return RedirectToAction("OnGet");
        }
    }
}
