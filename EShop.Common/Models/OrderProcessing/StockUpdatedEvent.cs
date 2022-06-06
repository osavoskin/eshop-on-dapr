using System;

namespace EShop.Common.Models.OrderProcessing
{
    public class StockUpdatedEvent
    {
        public Guid OrderId { get; set; }
    }
}
