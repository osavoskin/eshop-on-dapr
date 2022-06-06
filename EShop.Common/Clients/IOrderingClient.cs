using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EShop.Common.Clients
{
    public interface IOrderingClient
    {
        Task NotifyOrderSubmitted(Guid orderId, IDictionary<string, int> items, decimal sum);

        Task NotifyStockCheckRequested(Guid orderId, IDictionary<string, int> items);

        Task NotifyStockChecked(Guid orderId, IEnumerable<Guid> missingItems);

        Task NotifyPaymentRequested(Guid orderId, decimal sum);

        Task NotifyOrderPayed(Guid orderId, bool success);

        Task NotifyStockUpdateRequested(Guid orderId, IDictionary<string, int> soldItems);

        Task NotifyStockUpdated(Guid orderId);
    }
}
