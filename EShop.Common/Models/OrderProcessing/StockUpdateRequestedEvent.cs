using System;
using System.Collections.Generic;

namespace EShop.Common.Models.OrderProcessing
{
    public class StockUpdateRequestedEvent
    {
        public Guid OrderId { get; set; }

        public IDictionary<string, int> Items { get; set; }
    }
}
