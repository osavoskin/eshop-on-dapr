using Dapr;
using EShop.Common.Clients;
using EShop.Common.Models.OrderProcessing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace EShop.Ordering.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> logger;
        private readonly IOrderingClient orderingClient;

        public OrdersController(ILogger<OrdersController> logger, IOrderingClient orderingClient)
        {
            this.logger = logger;
            this.orderingClient = orderingClient;
        }

        [HttpPost]
        [Topic("pubsub", "orderSubmitted")]
        public async Task OnOrderSubmitted(OrderSubmittedEvent @event)
        {
            logger.LogInformation("New order submitted: " + @event.Order.Id);
            await orderingClient.NotifyStockCheckRequested(@event.Order);
        }
    }
}
