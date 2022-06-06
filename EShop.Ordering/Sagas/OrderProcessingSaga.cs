using Automatonymous;
using EShop.Common.Clients;
using EShop.Common.Models.OrderProcessing;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace EShop.Ordering.Sagas
{

    public class OrderProcessingSaga : AutomatonymousStateMachine<OrderProcessingSagaState>
    {
        private readonly ILogger<OrderProcessingSaga> logger;
        private readonly IOrderingClient orderingClient;

        public State StockBeingChecked { get; private set; }
        public State PaymentBeingProcessed { get; private set; }
        public State StockBeingUpdated { get; private set; }
        public State OrderProcessingSuccessful { get; private set; }
        public State OrderProcessingFailed { get; private set; }

        public Event<OrderSubmittedEvent> OrderSubmitted { get; private set; }
        public Event<StockCheckedEvent> StockChecked { get; private set; }
        public Event<StockCheckedEvent> StockEmpty { get; private set; }
        public Event<OrderPayedEvent> OrderPayed { get; private set; }
        public Event<OrderPayedEvent> NotEnoughMoney { get; private set; }
        public Event<StockUpdatedEvent> StockUpdated { get; private set; }

        public OrderProcessingSaga(ILogger<OrderProcessingSaga> logger, IOrderingClient orderingClient)
        {
            this.logger = logger;
            this.orderingClient = orderingClient;

            InstanceState(x => x.CurrentState);

            Initially(
                When(OrderSubmitted)
                    .TransitionTo(StockBeingChecked)
                    .ThenAsync(OnOrderSubmitted));

            During(StockBeingChecked,
                When(StockChecked)
                    .TransitionTo(PaymentBeingProcessed)
                    .ThenAsync(OnStockChecked),
                When(StockEmpty)
                    .TransitionTo(OrderProcessingFailed)
                    .Then(OnStockEmpty));

            During(PaymentBeingProcessed,
                When(OrderPayed)
                    .TransitionTo(StockBeingUpdated)
                    .ThenAsync(OnOrderPayed),
                When(NotEnoughMoney)
                    .TransitionTo(OrderProcessingFailed)
                    .Then(OnNotEnoughMoney));

            During(StockBeingUpdated,
                When(StockUpdated)
                    .TransitionTo(OrderProcessingSuccessful)
                    .Then(OnStockUpdated)
                    .Then(OnOrderCompleted));
        }

        private async Task OnOrderSubmitted(BehaviorContext<OrderProcessingSagaState, OrderSubmittedEvent> context)
        {
            logger.LogInformation("[SAGA] New order submitted: {0}",
                context.Data.OrderId);

            context.Instance.OrderId = context.Data.OrderId;
            context.Instance.Items = context.Data.Items;
            context.Instance.Sum = context.Data.Sum;

            await orderingClient.NotifyStockCheckRequested(
                context.Data.OrderId,
                context.Data.Items);

            logger.LogInformation("[SAGA] Stock check requested: {0}",
                context.Instance.OrderId);
        }

        private async Task OnStockChecked(BehaviorContext<OrderProcessingSagaState, StockCheckedEvent> context)
        {
            logger.LogInformation("[SAGA] Stock checked: {0}, can procede: {1}",
                context.Instance.OrderId,
                context.Data.CanProceedeWithOrder);

            if (context.Data.CanProceedeWithOrder)
            {
                await orderingClient.NotifyPaymentRequested(
                    context.Instance.OrderId,
                    context.Instance.Sum);

                logger.LogInformation("[SAGA] Payment requested: {0}, sum to pay: {1}",
                    context.Instance.OrderId,
                    context.Instance.Sum);
            }
            else
            {
                logger.LogError("[SAGA] Cannot paid the bill: {0}, sum to pay: {1}",
                    context.Instance.OrderId,
                    context.Instance.Sum);

                await context.Raise(StockEmpty);
            }
        }

        private async Task OnOrderPayed(BehaviorContext<OrderProcessingSagaState, OrderPayedEvent> context)
        {
            if (context.Data.Success)
            {
                logger.LogInformation("[SAGA] The order is paid in full: {0}, sum paid: {1}",
                    context.Instance.OrderId,
                    context.Instance.Sum);

                await orderingClient.NotifyStockUpdateRequested(
                    context.Instance.OrderId,
                    context.Instance.Items);

                logger.LogInformation("[SAGA] Items are sold, stock update is requested, order ID: {0}, items count: {1}",
                    context.Instance.OrderId,
                    context.Instance.Items.Count);
            }
            else
            {
                logger.LogError("[SAGA] Cannot paid the bill - not enough money: {0}, sum to pay: {1}",
                    context.Instance.OrderId,
                    context.Instance.Sum);

                await context.Raise(NotEnoughMoney);
            }
        }

        private void OnStockEmpty(BehaviorContext<OrderProcessingSagaState, StockCheckedEvent> context)
        {
            logger.LogError("[SAGA] Order processing completed with error: {0}",
                context.Instance.OrderId);
        }

        private void OnNotEnoughMoney(BehaviorContext<OrderProcessingSagaState, OrderPayedEvent> context)
        {
            logger.LogError("[SAGA] Order processing completed with error: {0}",
                context.Instance.OrderId);
        }

        private void OnStockUpdated(BehaviorContext<OrderProcessingSagaState, StockUpdatedEvent> context)
        {
            logger.LogInformation("[SAGA] Stock updated: {0}",
                context.Instance.OrderId);
        }

        private void OnOrderCompleted(BehaviorContext<OrderProcessingSagaState, StockUpdatedEvent> context)
        {
            logger.LogInformation("[SAGA] Order processing completed successfully: {0}",
                context.Instance.OrderId);
        }
    }
}
