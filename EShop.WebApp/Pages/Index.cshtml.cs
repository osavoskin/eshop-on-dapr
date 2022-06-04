using EShop.Common.Clients;
using EShop.Common.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EShop.WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> logger;
        private readonly ICatalogClient catalogClient;

        public IEnumerable<Item> CatalogItems { get; private set; }

        public IndexModel(ILogger<IndexModel> logger, ICatalogClient catalogClient)
        {
            this.logger = logger;
            this.catalogClient = catalogClient;
        }

        public async Task OnGet()
        {
            CatalogItems = await catalogClient.GetItems();
        }

        public async Task OnPostSubmit(Guid orderId)
        {
            Console.WriteLine(orderId);
        }
    }
}
