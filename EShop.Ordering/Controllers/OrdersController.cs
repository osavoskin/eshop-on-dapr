using Dapr;
using EShop.Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace EShop.Ordering.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> logger;

        public OrdersController(ILogger<OrdersController> logger)
        {
            this.logger = logger;
        }

        [HttpPost]
        [Topic("pubsub", "orderSubmitted")]
        public Task OnOrderSubmitted(Order order)
        {
            System.Console.WriteLine("Order received: " + order.Id);
            return Task.CompletedTask;
        }
    }
}
