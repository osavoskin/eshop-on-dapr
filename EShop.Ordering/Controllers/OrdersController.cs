using Dapr;
using Dapr.Actors;
using Dapr.Actors.Client;
using EShop.Common.Models.OrderProcessing;
using EShop.Ordering.Actors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EShop.Ordering.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IActorProxyFactory actorProxyFactory;

        public OrdersController(IActorProxyFactory actorProxyFactory)
        {
            this.actorProxyFactory = actorProxyFactory;
        }

        [HttpPost("orderSubmitted")]
        [Topic("pubsub", "orderSubmitted")]
        public async Task OnOrderSubmitted(OrderSubmittedEvent @event)
        {
            var actor = GetOrderingProcessActor(@event.OrderId);
            await actor.NotifyOrderSubmitted(@event);
        }

        [HttpPost("stockChecked")]
        [Topic("pubsub", "stockChecked")]
        public async Task OnStockChecked(StockCheckedEvent @event)
        {
            var actor = GetOrderingProcessActor(@event.OrderId);
            await actor.NotifyStockChecked(@event);
        }

        [HttpPost("orderPayed")]
        [Topic("pubsub", "orderPayed")]
        public async Task OnOrderPayed(OrderPayedEvent @event)
        {
            var actor = GetOrderingProcessActor(@event.OrderId);
            await actor.NotifyOrderPayed(@event);
        }

        [HttpPost("stockUpdated")]
        [Topic("pubsub", "stockUpdated")]
        public async Task OnStockUpdated(StockUpdatedEvent @event)
        {
            var actor = GetOrderingProcessActor(@event.OrderId);
            await actor.NotifyStockUpdated(@event);
        }

        private IOrderProcessorActor GetOrderingProcessActor(Guid orderId)
        {
            var actorId = new ActorId(orderId.ToString());
            return actorProxyFactory.CreateActorProxy<IOrderProcessorActor>(
                actorId,
                nameof(OrderProcessorActor));
        }
    }
}
