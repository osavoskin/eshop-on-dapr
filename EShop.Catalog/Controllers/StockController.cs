using Dapr;
using EShop.Common.Clients;
using EShop.Common.Models.OrderProcessing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace EShop.Catalog.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ILogger<StockController> logger;
        private readonly IOrderingClient orderingClient;

        public StockController(ILogger<StockController> logger, IOrderingClient orderingClient)
        {
            this.logger = logger;
            this.orderingClient = orderingClient;
        }

        [HttpPost("stockCheckRequested")]
        [Topic("pubsub", "stockCheckRequested")]
        public async Task OnStockCheckRequested(StockCheckRequestedEvent @event)
        {
            logger.LogInformation("Checked: the items are in stock");
            await orderingClient.NotifyStockChecked(missingItems: null);
        }

        [HttpPost("itemsSold")]
        [Topic("pubsub", "itemsSold")]
        public Task OnItemsSold(ItemsSoldEvent @event)
        {
            logger.LogInformation("Order processing completed");
            return Task.CompletedTask;
        }
    }
}
