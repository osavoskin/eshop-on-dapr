using Dapr.Client;
using EShop.Common.Models;
using System.Threading.Tasks;

namespace EShop.Common.Clients
{
    public class OrderingClient : IOrderingClient
    {
        private readonly DaprClient daprClient;

        public OrderingClient(DaprClient daprClient)
        {
            this.daprClient = daprClient;
        }
        public Task PlaceOrder(Order order)
        {
            return daprClient.PublishEventAsync("pubsub", "orderSubmitted", order);
        }
    }
}
