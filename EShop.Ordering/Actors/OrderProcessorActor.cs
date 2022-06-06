using Automatonymous;
using Dapr.Actors.Runtime;
using EShop.Common.Clients;
using EShop.Common.Models.OrderProcessing;
using EShop.Ordering.Sagas;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace EShop.Ordering.Actors
{
    public class OrderProcessorActor : Actor, IOrderProcessorActor
    {
        private readonly OrderProcessingSaga saga;

        public OrderProcessorActor(
            ActorHost host,
            ILogger<OrderProcessingSaga> logger,
            IOrderingClient orderingClient) : base(host)
        {
            saga = new OrderProcessingSaga(logger, orderingClient);
        }

        public async Task NotifyOrderSubmitted(OrderSubmittedEvent @event)
        {
            var state = await GetState(@event.OrderId);
            await saga.RaiseEvent(state, saga.OrderSubmitted, @event);
            await SaveState(state.OrderId, state);
        }

        public async Task NotifyStockChecked(StockCheckedEvent @event)
        {
            var state = await GetState(@event.OrderId);
            await saga.RaiseEvent(state, saga.StockChecked, @event);
            await SaveState(state.OrderId, state);
        }

        public async Task NotifyOrderPayed(OrderPayedEvent @event)
        {
            var state = await GetState(@event.OrderId);
            await saga.RaiseEvent(state, saga.OrderPayed, @event);
            await SaveState(state.OrderId, state);
        }

        public async Task NotifyStockUpdated(StockUpdatedEvent @event)
        {
            var state = await GetState(@event.OrderId);
            await saga.RaiseEvent(state, saga.StockUpdated, @event);
            await SaveState(state.OrderId, state);
        }

        private async Task<OrderProcessingSagaState> GetState(Guid orderId)
        {
            var key = GetKey(orderId);
            var state = await StateManager.TryGetStateAsync<OrderProcessingSagaState>(key);
            return state.Value ?? new OrderProcessingSagaState();
        }

        private Task SaveState(Guid orderId, OrderProcessingSagaState state)
        {
            var key = GetKey(orderId);
            return StateManager.SetStateAsync(key, state);
        }

        private static string GetKey(Guid orderId)
        {
            return "order-" + orderId;
        }
    }
}
