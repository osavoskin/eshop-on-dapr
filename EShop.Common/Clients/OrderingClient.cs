using Dapr.Client;
using EShop.Common.Models;
using EShop.Common.Models.OrderProcessing;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EShop.Common.Clients
{
    public class OrderingClient : IOrderingClient
    {
        private const string PubsubName = "pubsub";
        private readonly DaprClient daprClient;

        public OrderingClient(DaprClient daprClient)
        {
            this.daprClient = daprClient;
        }

        public Task NotifyOrderSubmitted(Order order)
        {
            return daprClient.PublishEventAsync(PubsubName, "orderSubmitted",
                new OrderSubmittedEvent
                {
                    Order = order
                });
        }

        public Task NotifyStockCheckRequested(Order order)
        {
            return daprClient.PublishEventAsync(PubsubName, "stockCheckRequested",
                new StockCheckRequestedEvent
                {
                    Order = order
                });
        }

        public Task NotifyStockChecked(IEnumerable<Guid> missingItems)
        {
            return daprClient.PublishEventAsync(PubsubName, "stockChecked",
                new StockCheckedEvent
                {
                    MissingItems = missingItems
                });
        }

        public Task NotifyPaymentRequested(Guid orderId, decimal sum)
        {
            return daprClient.PublishEventAsync(PubsubName, "paymentRequested",
                new PaymentRequestedEvent
                {
                    OrderId = orderId,
                    Sum = sum
                });
        }

        public Task NotifyOrderPayed(Guid orderId, bool success)
        {
            return daprClient.PublishEventAsync(PubsubName, "orderPayed",
                new OrderPayedEvent
                {
                    OrderId = orderId,
                    Success = success
                });
        }

        public Task NotifyItemsSold(IDictionary<Guid, int> soldItems)
        {
            return daprClient.PublishEventAsync(PubsubName, "itemsSold",
                new ItemsSoldEvent
                {
                    Items = soldItems
                });
        }
    }
}
