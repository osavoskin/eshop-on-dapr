using Dapr.Client;
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

        public Task NotifyOrderSubmitted(Guid orderId, IDictionary<string, int> items, decimal sum)
        {
            return daprClient.PublishEventAsync(PubsubName, "orderSubmitted",
                new OrderSubmittedEvent
                {
                    OrderId = orderId,
                    Items = items,
                    Sum = sum
                });
        }

        public Task NotifyStockCheckRequested(Guid orderId, IDictionary<string, int> items)
        {
            return daprClient.PublishEventAsync(PubsubName, "stockCheckRequested",
                new StockCheckRequestedEvent
                {
                    OrderId = orderId,
                    Items = items,
                });
        }

        public Task NotifyStockChecked(Guid orderId, IEnumerable<Guid> missingItems)
        {
            return daprClient.PublishEventAsync(PubsubName, "stockChecked",
                new StockCheckedEvent
                {
                    OrderId = orderId,
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

        public Task NotifyStockUpdateRequested(Guid orderId, IDictionary<string, int> soldItems)
        {
            return daprClient.PublishEventAsync(PubsubName, "stockUpdateRequested",
                new StockUpdateRequestedEvent
                {
                    OrderId = orderId,
                    Items = soldItems
                });
        }

        public Task NotifyStockUpdated(Guid orderId)
        {
            return daprClient.PublishEventAsync(PubsubName, "stockUpdated",
                new StockUpdatedEvent
                {
                    OrderId = orderId
                });
        }
    }
}
