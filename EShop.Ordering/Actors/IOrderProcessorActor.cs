using Dapr.Actors;
using EShop.Common.Models.OrderProcessing;
using System.Threading.Tasks;

namespace EShop.Ordering.Actors
{
    public interface IOrderProcessorActor : IActor
    {
        Task NotifyOrderSubmitted(OrderSubmittedEvent @event);

        Task NotifyStockChecked(StockCheckedEvent @event);

        Task NotifyOrderPayed(OrderPayedEvent @event);

        Task NotifyStockUpdated(StockUpdatedEvent @event);
    }
}