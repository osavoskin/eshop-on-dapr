using Dapr;
using EShop.Common.Clients;
using EShop.Common.Models.OrderProcessing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace EShop.Payment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly ILogger<PaymentsController> logger;
        private readonly IOrderingClient orderingClient;

        public PaymentsController(ILogger<PaymentsController> logger, IOrderingClient orderingClient)
        {
            this.logger = logger;
            this.orderingClient = orderingClient;
        }

        [HttpPost("paymentRequested")]
        [Topic("pubsub", "paymentRequested")]
        public async Task OnPaymentRequested(PaymentRequestedEvent @event)
        {
            logger.LogInformation("Processing payment... Done");
            await orderingClient.NotifyOrderPayed(@event.OrderId, true);
        }
    }
}
