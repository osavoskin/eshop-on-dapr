using EShop.Common.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EShop.Common.Clients
{
    public interface IOrderingClient
    {
        Task NotifyOrderSubmitted(Order order);

        Task NotifyStockCheckRequested(Order order);

        Task NotifyStockChecked(IEnumerable<Guid> missingItems);

        Task NotifyPaymentRequested(Guid orderId, decimal sum);

        Task NotifyOrderPayed(Guid orderId, bool success);
        
        Task NotifyItemsSold(IDictionary<Guid, int> soldItems);
    }
}
