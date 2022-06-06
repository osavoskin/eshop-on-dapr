using System;
using System.Collections.Generic;

namespace EShop.Common.Models.OrderProcessing
{
    public class StockCheckRequestedEvent
    {
        public Guid OrderId { get; set; }

        public IDictionary<string, int> Items { get; set; }
    }
}
