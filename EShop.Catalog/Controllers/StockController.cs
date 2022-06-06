using Dapr;
using EShop.Common.Clients;
using EShop.Common.Models.OrderProcessing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace EShop.Catalog.Controllers
{
    [Route("api/[controller]")]
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
        public Task OnStockCheckRequested(StockCheckRequestedEvent @event)
        {
            logger.LogInformation("Checked: the items are in stock");
            return orderingClient.NotifyStockChecked(@event.OrderId, missingItems: null);
        }

        [HttpPost("stockUpdateRequested")]
        [Topic("pubsub", "stockUpdateRequested")]
        public Task OnItemsSold(StockUpdateRequestedEvent @event)
        {
            logger.LogInformation("Stock being updated... Done");
            return orderingClient.NotifyStockUpdated(@event.OrderId);
        }
    }
}
