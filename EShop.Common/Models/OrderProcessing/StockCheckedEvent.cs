using System;
using System.Collections.Generic;
using System.Linq;

namespace EShop.Common.Models.OrderProcessing
{
    public class StockCheckedEvent
    {
        public Guid OrderId { get; set; }

        public IEnumerable<Guid> MissingItems { get; set; }

        public IDictionary<string, int> InStockItems { get; set; }

        public bool CanProceedeWithOrder
        {
            get => MissingItems is null || MissingItems.Count() == 0;
        }
    }
}
